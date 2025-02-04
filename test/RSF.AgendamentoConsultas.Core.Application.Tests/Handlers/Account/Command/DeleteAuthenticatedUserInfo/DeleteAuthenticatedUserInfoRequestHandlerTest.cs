using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.DeleteAuthenticatedUserInfo;
using Moq;
using FluentAssertions;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.DeleteAuthenticatedUserInfo;

public class DeleteAuthenticatedUserInfoRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;
    private readonly Mock<IEspecialistaRepository> _especialistaRepositoryMock;
    private readonly DeleteAuthenticatedUserInfoRequestHandler _handler;

    public DeleteAuthenticatedUserInfoRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        _eventBusMock = new Mock<IEventBus>();
        _pacienteRepositoryMock = new Mock<IPacienteRepository>();
        _especialistaRepositoryMock = new Mock<IEspecialistaRepository>();

        _handler = new DeleteAuthenticatedUserInfoRequestHandler(
            _fixture.HttpContextAccessorMockAdmin.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object,
            _accountManagerServiceMock.Object,
            _pacienteRepositoryMock.Object,
            _especialistaRepositoryMock.Object);
    }

    [Fact(DisplayName = "Deve desativar a conta do paciente e publicar evento com sucesso")]
    public async Task Handle_ShouldDeactivateAccountForPacienteAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange
        var user = _fixture.UserPaciente;
        var paciente = _fixture.Paciente;
        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
        _accountManagerServiceMock.Setup(x => x.DeactivateUserAccountAsync(user)).ReturnsAsync(true);
        _pacienteRepositoryMock.Setup(c => c.GetByUserIdAsync(user.Id)).ReturnsAsync(paciente);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:DeactivateAccountQueueName", "ha.queue.mock_queue_name");

        var handler = new DeleteAuthenticatedUserInfoRequestHandler(
            _fixture.HttpContextAccessorMockPaciente.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object,
            _accountManagerServiceMock.Object,
            _pacienteRepositoryMock.Object,
            _especialistaRepositoryMock.Object
            );

        // Act
        var result = await handler.Handle(new DeleteAuthenticatedUserInfoRequest(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeTrue();

        _eventBusMock.Verify(x => x.Publish(It.IsAny<DesativarContaCreatedEvent>(), "ha.queue.mock_queue_name", ""), Times.Once);
        _pacienteRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Once);
        _especialistaRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact(DisplayName = "Deve desativar a conta do especialista e publicar evento com sucesso")]
    public async Task Handle_ShouldDeactivateAccountForEspecialistaAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange
        var user = _fixture.UserEspecialista;
        var especialista = _fixture.Especialista;
        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
        _accountManagerServiceMock.Setup(x => x.DeactivateUserAccountAsync(user)).ReturnsAsync(true);
        _especialistaRepositoryMock.Setup(c => c.GetByUserIdAsync(user.Id)).ReturnsAsync(especialista);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:DeactivateAccountQueueName", "ha.queue.mock_queue_name");

        var handler = new DeleteAuthenticatedUserInfoRequestHandler(
            _fixture.HttpContextAccessorMockEspecialista.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object,
            _accountManagerServiceMock.Object,
            _pacienteRepositoryMock.Object,
            _especialistaRepositoryMock.Object
            );

        // Act
        var result = await handler.Handle(new DeleteAuthenticatedUserInfoRequest(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeTrue();

        _eventBusMock.Verify(x => x.Publish(It.IsAny<DesativarContaCreatedEvent>(), "ha.queue.mock_queue_name", ""), Times.Once);
        _especialistaRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Once);
        _pacienteRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact(DisplayName = "Deve desativar a conta do Admin e publicar evento com sucesso")]
    public async Task Handle_ShouldDeactivateAccountForAdminAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange
        var user = _fixture.UserAdmin;
        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
        _accountManagerServiceMock.Setup(x => x.DeactivateUserAccountAsync(user)).ReturnsAsync(true);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:DeactivateAccountQueueName", "ha.queue.mock_queue_name");

        // Act
        var result = await _handler.Handle(new DeleteAuthenticatedUserInfoRequest(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeTrue();

        _eventBusMock.Verify(x => x.Publish(It.IsAny<DesativarContaCreatedEvent>(), "ha.queue.mock_queue_name", ""), Times.Once);
        _pacienteRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Never);
    }



    [Fact(DisplayName = "Deve retornar erro ao falhar na desativação da conta")]
    public async Task Handle_ShouldReturnError_WhenDeleteAuthenticatedUserInfoRequestItFails()
    {
        // Arrange
        var user = _fixture.UserAdmin;
        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
        _accountManagerServiceMock.Setup(x => x.DeactivateUserAccountAsync(user)).ReturnsAsync(false);
                
        // Act
        var result = await _handler.Handle(new DeleteAuthenticatedUserInfoRequest(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeFalse();
        result.Exception.Should().BeOfType<OperationErrorException>()
            .Which.Message.Should().Be("Falha ao inativar o usuário.");

        _pacienteRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<DesativarContaCreatedEvent>(), "ha.queue.mock_queue_name", ""), Times.Never);
    }


    [Fact(DisplayName = "Deve lançar exceção UnauthorizedRequestException quando usuário não está autenticado")]
    public async Task Handle_ShouldThrowUnauthorizedRequestException_WhenUserIsNotAuthenticated()
    {
        // Arrange
        Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());
        
        var handler = new DeleteAuthenticatedUserInfoRequestHandler(
            httpContextAccessorMock.Object,
            _eventBusMock.Object,
            new Mock<IConfiguration>().Object,
            _accountManagerServiceMock.Object,
            _pacienteRepositoryMock.Object,
            _especialistaRepositoryMock.Object);

        // Act
        var act = async () => await handler.Handle(new DeleteAuthenticatedUserInfoRequest(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedRequestException>()
            .WithMessage("Usuário não está autenticado na plataforma");

        _accountManagerServiceMock.Verify(x => x.DeactivateUserAccountAsync(It.IsAny<ApplicationUser>()), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<DesativarContaCreatedEvent>(), "ha.queue.mock_queue_name", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve lançar exceção NotFoundException quando usuário não é encontrado")]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserIsNotFound()
    {
        // Arrange
        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()));

        // Act
        var act = async () => await _handler.Handle(new DeleteAuthenticatedUserInfoRequest(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Usuário não está autenticado na plataforma");

        _accountManagerServiceMock.Verify(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.DeactivateUserAccountAsync(It.IsAny<ApplicationUser>()), Times.Never);
        _pacienteRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Never);
        _especialistaRepositoryMock.Verify(x => x.GetByUserIdAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<DesativarContaCreatedEvent>(), "ha.queue.mock_queue_name", ""), Times.Never);
    }
}