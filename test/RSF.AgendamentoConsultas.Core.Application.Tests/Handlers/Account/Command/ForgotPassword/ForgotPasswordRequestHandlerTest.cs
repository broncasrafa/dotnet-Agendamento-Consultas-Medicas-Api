using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ForgotPassword;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using Moq;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.ForgotPassword;

public class ForgotPasswordRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly ForgotPasswordRequestHandler _handler;
    private readonly ForgotPasswordRequest _request;
    private const string _resetPasswordCode = "abd89b5267850a53f2b1bb908c4b898ef34eb550";

    public ForgotPasswordRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        _eventBusMock = new Mock<IEventBus>();
        _request = new ForgotPasswordRequest(_fixture.Faker.Person.Email);
        _handler = new ForgotPasswordRequestHandler(_accountManagerServiceMock.Object, _eventBusMock.Object, _fixture.ConfigurationMock.Object);
    }


    [Fact(DisplayName = "Deve reenviar código de reset de senha com sucesso e deve publicar evento na fila")]
    public async Task Handle_ShouldSendPasswordResetToken_WhenUserExists()
    {
        // Arrange
        var user = _fixture.UserAdmin;
        _accountManagerServiceMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _accountManagerServiceMock.Setup(x => x.ForgotPasswordAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(_resetPasswordCode);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:ForgotPasswordQueueName", "ha.queue.mock_queuename");

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeTrue();

        _accountManagerServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.ForgotPasswordAsync(It.IsAny<ApplicationUser>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<ForgotPasswordCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Once);
    }

    [Fact(DisplayName = "Não foi possível obter o código de confirmação de e-mail e não deve publicar evento na fila")]
    public async Task Handle_ShouldNotSendPasswordResetTokenAndNotPublishEvent_WhenRequestIsFalse()
    {
        // Arrange
        var user = _fixture.UserAdmin;
        _accountManagerServiceMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _accountManagerServiceMock.Setup(x => x.ForgotPasswordAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(string.Empty);

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeFalse();

        _accountManagerServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.ForgotPasswordAsync(It.IsAny<ApplicationUser>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<ForgotPasswordCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }

    [Fact(DisplayName = "Deve lançar exceção NotFoundException quando usuário não é encontrado")]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserIsNotFound()
    {
        // Arrange
        _accountManagerServiceMock.Setup(x => x.FindByEmailAsync(_request.Email));

        // Act
        var act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Usuário com o e-mail '{_request.Email}' não encontrado");

        _accountManagerServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.ForgotPasswordAsync(It.IsAny<ApplicationUser>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<ForgotPasswordCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }
}