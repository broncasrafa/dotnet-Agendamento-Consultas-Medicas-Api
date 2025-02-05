using System.Linq.Expressions;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterPaciente;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base.Extensions;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using Moq;
using Bogus.Extensions.Belgium;
using FluentAssertions;
using Bogus.Extensions.Brazil;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.RegisterPaciente;

public class RegisterPacienteRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly RegisterPacienteRequestHandler _handler;
    private readonly RegisterPacienteRequest _request;
    private readonly UsuarioAutenticadoModel _usuarioAutenticadoModelMock;

    public RegisterPacienteRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        _pacienteRepositoryMock = new Mock<IPacienteRepository>();
        
        _eventBusMock = new Mock<IEventBus>();

        _handler = new RegisterPacienteRequestHandler(
            _accountManagerServiceMock.Object,
            _pacienteRepositoryMock.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object);

        _request = new RegisterPacienteRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                CPF: _fixture.Faker.Person.Cpf(),
                Username: _fixture.Faker.Person.UserName,
                Email: _fixture.Faker.Person.Email,
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Genero: _fixture.Faker.CustomGender(),
                DataNascimento: Convert.ToDateTime(_fixture.Faker.CustomDateOfBirth()),
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

        _usuarioAutenticadoModelMock = new UsuarioAutenticadoModel
        {
            Id = _fixture.UserId,
            Nome = _fixture.Faker.Person.FullName,
            Documento = _fixture.Faker.Person.Cpf(),
            Username = _fixture.Faker.Person.UserName,
            Email = _fixture.Faker.Person.Email,
            Telefone = _fixture.Faker.Person.CustomCellPhoneBR(),
            IsActive = true,
            CreatedAt = DateTime.Now.AddDays(-7),
            Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9"
        };
    }


    [Fact(DisplayName = "Deve registrar o novo usuário e publicar evento com sucesso")]
    public async Task Handle_ShouldRegisterAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange 
        var authenticatedUser = _usuarioAutenticadoModelMock;

        _accountManagerServiceMock.SetupSequence(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .Returns(Task.FromResult<ApplicationUser?>(null))
            .Returns(Task.FromResult<ApplicationUser?>(null));
        _accountManagerServiceMock.Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>())).ReturnsAsync("NyeVfu3nZTZizATtjZtSbTEU");
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:EmailConfirmationQueueName", "ha.queue.mock_queuename");

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value!.Usuario.Should().NotBeNull();
        
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Exactly(2));
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Once);
    }

    [Fact(DisplayName = "Deve registrar o novo usuário e se o código de confirmação de e-mail não retornar não deve publicar evento")]
    public async Task Handle_ShouldRegisterAndNotPublishEvent_WhenEmailConfirmationTokenIsNullOrEmpty()
    {
        // Arrange 
        var authenticatedUser = _usuarioAutenticadoModelMock;

        _accountManagerServiceMock.SetupSequence(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .Returns(Task.FromResult<ApplicationUser?>(null))
            .Returns(Task.FromResult<ApplicationUser?>(null));
        _accountManagerServiceMock.Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()));

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value!.Usuario.Should().NotBeNull();

        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Exactly(2));
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve lançar exceção se o e-mail já estiver cadastrado")]
    public async Task Handle_ShouldThrowException_When_EmailAlreadyExists()
    {
        // Arrange
        var user = _fixture.UserPaciente;
        _accountManagerServiceMock.SetupSequence(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .Returns(Task.FromResult<ApplicationUser?>(null))
            .ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage($"Usuário já cadastrado");

        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Exactly(2));
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve lançar exceção se o CPF já estiver cadastrado")]
    public async Task Handle_ShouldThrowException_When_CpfAlreadyExists()
    {
        // Arrange
        var user = _fixture.UserPaciente;

        _accountManagerServiceMock.SetupSequence(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage($"Usuário já cadastrado");
        
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Exactly(1));
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve retornar erro se o registro falhar")]
    public async Task Handle_ShouldReturnError_WhenRegistrationFails()
    {
        // Arrange
        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()));
        _accountManagerServiceMock.Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()))
            .ReturnsAsync((UsuarioAutenticadoModel)default!);

        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()));

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exception.Should().BeOfType<OperationErrorException>()
            .Which.Message.Should().Be("Falha ao realizar o registro do novo usuário.");
    }
}