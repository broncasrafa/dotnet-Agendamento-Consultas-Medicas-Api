using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ChangePassword;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using FluentAssertions;
using Moq;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.ChangePassword;

public class ChangePasswordRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly BaseFixture _fixture;
    private readonly ChangePasswordRequestHandler _handler;
    private readonly ChangePasswordRequest _request;

    public ChangePasswordRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        
        _eventBusMock = new Mock<IEventBus>();

        _configurationMock = new Mock<IConfiguration>();

        _handler = new ChangePasswordRequestHandler(
            _accountManagerServiceMock.Object,
            _fixture.HttpContextAccessorMockPaciente.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object
        );

        _request = new ChangePasswordRequest(NewPassword: "NewPassword456", OldPassword: "OldPassword123");
    }


    [Fact(DisplayName = "Deve alterar senha e publicar evento com sucesso")]
    public async Task Handle_ShouldChangePasswordAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange
        var user = _fixture.UserPaciente;        
        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
        _accountManagerServiceMock.Setup(x => x.ChangePasswordAsync(user, "OldPassword123", "NewPassword456")).ReturnsAsync(true);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:ChangePasswordQueueName", "ha.queue.mudanca_senha");

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeTrue();

        _eventBusMock.Verify(x => x.Publish(It.IsAny<ChangePasswordCreatedEvent>(), "ha.queue.mudanca_senha", ""), Times.Once);
    }

    [Fact(DisplayName = "Não foi possível alterar a senha e não deve publicar evento na fila")]
    public async Task Handle_ShouldChangePasswordAndNotPublishEvent_WhenRequestIsFalse()
    {
        // Arrange
        var user = _fixture.UserPaciente;
        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
        _accountManagerServiceMock.Setup(x => x.ChangePasswordAsync(user, "OldPassword123", "NewPassword456")).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeFalse();

        _eventBusMock.Verify(x => x.Publish(It.IsAny<ChangePasswordCreatedEvent>(), "ha.queue.mudanca_senha", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve lançar exceção UnauthorizedRequestException quando usuário não está autenticado")]
    public async Task Handle_ShouldThrowUnauthorizedRequestException_WhenUserIsNotAuthenticated()
    {
        // Arrange
        Mock<IHttpContextAccessor> httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());
        var handler = new ChangePasswordRequestHandler(
            _accountManagerServiceMock.Object,
            httpContextAccessorMock.Object,
            _eventBusMock.Object,
            _configurationMock.Object
        );

        // Act
        var act = async () => await handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedRequestException>()
            .WithMessage("Usuário não está autenticado na plataforma");
    }

    [Fact(DisplayName = "Deve lançar exceção NotFoundException quando usuário não é encontrado")]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserIsNotFound()
    {
        // Arrange
        _accountManagerServiceMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()));

        // Act
        var act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Usuário não está autenticado na plataforma");
    }
}