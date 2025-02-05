using System.Linq.Expressions;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterEspecialista;
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

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.RegisterEspecialista;

public class RegisterEspecialistaRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly Mock<IEspecialistaRepository> _especialistaRepositoryMock;
    private readonly Mock<IEspecialidadeRepository> _especialidadeRepositoryMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly RegisterEspecialistaRequestHandler _handler;
    private readonly RegisterEspecialistaRequest _request;
    private readonly Especialidade _especialidadeMock;
    private readonly UsuarioAutenticadoModel _usuarioAutenticadoModelMock;

    public RegisterEspecialistaRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        _especialistaRepositoryMock = new Mock<IEspecialistaRepository>();
        _especialidadeRepositoryMock = new Mock<IEspecialidadeRepository>();
        _eventBusMock = new Mock<IEventBus>();

        _handler = new RegisterEspecialistaRequestHandler(
            _accountManagerServiceMock.Object,
            _especialistaRepositoryMock.Object,
            _especialidadeRepositoryMock.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object);

        _request = new RegisterEspecialistaRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                Username: _fixture.Faker.Person.UserName,
                Email: _fixture.Faker.Person.Email,
                TipoCategoria: _fixture.Faker.PickRandom(new string[] { "BASIC", "PREMIUM" }),
                EspecialidadeId: 1,
                Genero: _fixture.Faker.CustomGender(),
                Licenca: _fixture.Faker.Person.NationalNumber(),
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

        _usuarioAutenticadoModelMock = new UsuarioAutenticadoModel
        {
            Id = _fixture.UserId,
            Nome = _fixture.Faker.Person.FullName,
            Documento = _fixture.Faker.Person.NationalNumber(),
            Username = _fixture.Faker.Person.UserName,
            Email = _fixture.Faker.Person.Email,
            Telefone = _fixture.Faker.Person.CustomCellPhoneBR(),
            IsActive = true,
            CreatedAt = DateTime.Now.AddDays(-7),
            Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9"
        };

        _especialidadeMock = new Especialidade(1, _fixture.Faker.Company.CompanyName(), _fixture.Faker.Company.CompanyName(), 1);
    }


    [Fact(DisplayName = "Deve registrar o novo usuário e publicar evento com sucesso")]
    public async Task Handle_ShouldRegisterAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange 
        var authenticatedUser = _usuarioAutenticadoModelMock;

        _especialidadeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialidadeMock);
        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()));
        _accountManagerServiceMock.Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>())).ReturnsAsync("NyeVfu3nZTZizATtjZtSbTEU");
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:EmailConfirmationQueueName", "ha.queue.mock_queuename");

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value!.Usuario.Should().NotBeNull();

        _especialidadeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
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

        _especialidadeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialidadeMock);
        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()));
        _accountManagerServiceMock.Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()));

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value!.Usuario.Should().NotBeNull();

        _especialidadeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Exactly(2));
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve lançar exceção se especialidade não for encontrada")]
    public async Task Handle_ShouldThrowException_When_EspecialidadeNotFound()
    {
        // Arrange
        _especialidadeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Especialidade com o ID: '{_request.EspecialidadeId}' não encontrada");

        _especialidadeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve lançar exceção se o e-mail já estiver cadastrado")]
    public async Task Handle_ShouldThrowException_When_EmailAlreadyExists()
    {
        // Arrange
        var user = _fixture.UserEspecialista;
        _especialidadeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialidadeMock);
        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage($"Usuário já cadastrado");

        _especialidadeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve lançar exceção se a licença já estiver cadastrada")]
    public async Task Handle_ShouldThrowException_When_LicencaAlreadyExists()
    {
        // Arrange
        var user = _fixture.UserEspecialista;
        _especialidadeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialidadeMock);
        _accountManagerServiceMock.SetupSequence(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
            .Returns(Task.FromResult<ApplicationUser?>(null))
            .ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage($"Usuário já cadastrado");

        _especialidadeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Exactly(2));
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve retornar erro se o registro falhar")]
    public async Task Handle_ShouldReturnError_WhenRegistrationFails()
    {
        // Arrange
        _especialidadeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialidadeMock);
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