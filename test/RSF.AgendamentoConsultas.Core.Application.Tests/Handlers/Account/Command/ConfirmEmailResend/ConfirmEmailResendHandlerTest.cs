using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmailResend;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using Moq;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.ConfirmEmailResend;

public class ConfirmEmailResendHandlerTest : IClassFixture<BaseFixture>
{
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly BaseFixture _fixture;
    private readonly ConfirmEmailResendRequest _request;
    private readonly ConfirmEmailResendRequestHandler _handler;
    private const string _confirmationCode = "abd89b5267850a53f2b1bb908c4b898ef34eb550";

    public ConfirmEmailResendHandlerTest(BaseFixture fixture)
    {
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        _eventBusMock = new Mock<IEventBus>();
        _fixture = fixture;
        _handler = new ConfirmEmailResendRequestHandler(_eventBusMock.Object, _fixture.ConfigurationMock.Object, _accountManagerServiceMock.Object);
        _request = new ConfirmEmailResendRequest(Email: _fixture.Faker.Person.Email);
    }

    [Fact(DisplayName = "Deve reenviar código de confirmação de e-mail com sucesso")]
    public async Task Handle_ShouldResendEmailConfirmationToken_WhenUserExists()
    {
        // Arrange
        var user = _fixture.UserPaciente;

        _accountManagerServiceMock.Setup(x => x.FindByEmailAsync(_request.Email)).ReturnsAsync(user);
        _accountManagerServiceMock.Setup(x => x.ResendEmailConfirmationTokenAsync(_request.Email)).ReturnsAsync(_confirmationCode);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:EmailConfirmationQueueName", "ha.queue.email_confirmation");

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeTrue();

        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.email_confirmation", ""), Times.Once);
    }

    [Fact(DisplayName = "Não foi possível obter o código de confirmação de e-mail e não deve publicar evento na fila")]
    public async Task Handle_ShouldNotResendEmailConfirmationTokenAndNotPublishEvent_WhenRequestIsFalse()
    {
        // Arrange
        var user = _fixture.UserPaciente;
        _accountManagerServiceMock.Setup(x => x.FindByEmailAsync(_request.Email)).ReturnsAsync(user);
        _accountManagerServiceMock.Setup(x => x.ResendEmailConfirmationTokenAsync(_request.Email)).ReturnsAsync(string.Empty);

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeFalse();

        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.email_confirmation", ""), Times.Never);
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

        _accountManagerServiceMock.Verify(x => x.ResendEmailConfirmationTokenAsync(It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<EmailConfirmationCreatedEvent>(), "ha.queue.email_confirmation", ""), Times.Never);
    }
}