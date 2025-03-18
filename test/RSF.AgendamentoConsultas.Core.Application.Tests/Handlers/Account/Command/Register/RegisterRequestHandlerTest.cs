using System.Linq.Expressions;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Register;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base.Extensions;
using Moq;
using Bogus.Extensions.Brazil;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.Register;

public class RegisterRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly BaseFixture _fixture;
    private readonly RegisterRequestHandler _handler;

    public RegisterRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        _eventBusMock = new Mock<IEventBus>();

        _handler = new RegisterRequestHandler(_accountManagerServiceMock.Object, _eventBusMock.Object, _fixture.ConfigurationMock.Object);
    }

    [Fact(DisplayName = "Deve registrar o novo usuário e publicar evento com sucesso")]
    public async Task Handle_ShouldRegisterAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange
        var request = new RegisterRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                CPF: _fixture.Faker.Person.Cpf(),
                Username: _fixture.Faker.Person.UserName,
                Email: _fixture.Faker.Person.Email,
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Genero: _fixture.Faker.CustomGender(),
                TipoAcesso: "Administrador",
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");
        var authenticatedUser = new UsuarioAutenticadoModel
        {
            UserId = _fixture.UserId,
            Nome = _fixture.Faker.Person.FullName,
            Documento = _fixture.Faker.Person.Cpf(),
            Username = _fixture.Faker.Person.UserName,
            Email = _fixture.Faker.Person.Email,
            Telefone = _fixture.Faker.Person.CustomCellPhoneBR(),
            IsActive = true,
            CreatedAt = DateTime.Now.AddDays(-7),
            Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9"
        };

        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()));
        _accountManagerServiceMock.Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>())).ReturnsAsync("NyeVfu3nZTZizATtjZtSbTEU");
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:EmailConfirmationQueueName", "ha.queue.mock_queuename");

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value!.Usuario.Should().NotBeNull();

        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Once);
    }

    [Fact(DisplayName = "Deve lançar a exceção AlreadyExistsException para usuário já cadastrado")]
    public async Task Handle_ShouldThrowException_When_Usuario_AlreadyExists()
    {
        // Arrange
        var user = _fixture.UserAdmin;
        var request = new RegisterRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                CPF: _fixture.Faker.Person.Cpf(),
                Username: _fixture.Faker.Person.UserName,
                Email: _fixture.Faker.Person.Email,
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Genero: _fixture.Faker.CustomGender(),
                TipoAcesso: "Administrador",
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<AlreadyExistsException>()
            .WithMessage("Usuário já cadastrado");

        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Never);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve registrar o novo usuário e não deve publicar evento enviando o codigo de confirmação de email")]
    public async Task Handle_ShouldRegisterAndNotPublishEvent_When_CodeEmailConfirmation_IsNullOrEmpty()
    {
        // Arrange
        var request = new RegisterRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                CPF: _fixture.Faker.Person.Cpf(),
                Username: _fixture.Faker.Person.UserName,
                Email: _fixture.Faker.Person.Email,
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Genero: _fixture.Faker.CustomGender(),
                TipoAcesso: "Administrador",
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");
        var authenticatedUser = new UsuarioAutenticadoModel
        {
            UserId = _fixture.UserId,
            Nome = _fixture.Faker.Person.FullName,
            Documento = _fixture.Faker.Person.Cpf(),
            Username = _fixture.Faker.Person.UserName,
            Email = _fixture.Faker.Person.Email,
            Telefone = _fixture.Faker.Person.CustomCellPhoneBR(),
            IsActive = true,
            CreatedAt = DateTime.Now.AddDays(-7),
            Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9"
        };

        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()));
        _accountManagerServiceMock.Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>())).ReturnsAsync(authenticatedUser);
        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value!.Usuario.Should().NotBeNull();

        _accountManagerServiceMock.Verify(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve retornar erro se o registro falhar")]
    public async Task Handle_ShouldReturnError_WhenRegistrationFails()
    {
        // Arrange
        var request = new RegisterRequest(
                NomeCompleto: _fixture.Faker.Person.FullName,
                CPF: _fixture.Faker.Person.Cpf(),
                Username: _fixture.Faker.Person.UserName,
                Email: _fixture.Faker.Person.Email,
                Telefone: _fixture.Faker.Person.CustomCellPhoneBR(),
                Genero: _fixture.Faker.CustomGender(),
                TipoAcesso: "Administrador",
                Password: "Usuario@123",
                ConfirmPassword: "Usuario@123");

        _accountManagerServiceMock.Setup(x => x.CheckIfAlreadyExistsByFilterAsync(It.IsAny<Expression<Func<ApplicationUser, bool>>>()));
        _accountManagerServiceMock.Setup(x => x.RegisterAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ETipoPerfilAcesso>()));
        _accountManagerServiceMock.Setup(x => x.GetEmailConfirmationTokenAsync(It.IsAny<string>()));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exception.Should().BeOfType<OperationErrorException>()
            .Which.Message.Should().Be("Falha ao realizar o registro do novo usuário.");
    }
}