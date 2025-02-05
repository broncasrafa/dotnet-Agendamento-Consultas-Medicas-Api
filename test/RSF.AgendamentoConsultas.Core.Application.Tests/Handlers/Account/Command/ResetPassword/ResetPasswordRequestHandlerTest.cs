using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ResetPassword;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Application.Tests.Base;
using Moq;
using FluentAssertions;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using System.Linq.Expressions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Models;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Account.Command.ResetPassword;

public class ResetPasswordRequestHandlerTest : IClassFixture<BaseFixture>
{
    private readonly BaseFixture _fixture;
    private readonly Mock<IAccountManagerService> _accountManagerServiceMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly ResetPasswordRequest _request;
    private readonly ResetPasswordRequestHandler _handler;

    public ResetPasswordRequestHandlerTest(BaseFixture fixture)
    {
        _fixture = fixture;
        _accountManagerServiceMock = new Mock<IAccountManagerService>();
        _eventBusMock = new Mock<IEventBus>();

        _handler = new ResetPasswordRequestHandler(
            _accountManagerServiceMock.Object, _eventBusMock.Object, _fixture.ConfigurationMock.Object);

        _request = new ResetPasswordRequest(_fixture.Faker.Person.Email, "EyCqDm97SgPzGH9tmrNCPEQe", NewPassword: "EyCqDm97SgPzGH9tmrNCPEQe");
    }


    [Fact(DisplayName = "Deve resetar a senha do usuario com sucesso")]
    public async Task Handle_ShouldResetPasswordSuccessfully_WhenRequestIsValid()
    {
        // Arrange 
        var applicationUser = _fixture.UserAdmin;

        _accountManagerServiceMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);
        _accountManagerServiceMock.Setup(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:ChangePasswordQueueName", "ha.queue.mock_queuename");

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value.Should().BeTrue();

        _accountManagerServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<ChangePasswordCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Once);
    }


    [Fact(DisplayName = "Deve resetar a senha do usuario e publicar evento com sucesso")]
    public async Task Handle_ShouldResetPasswordSuccessfullyAndPublishEvent_WhenRequestIsValid()
    {
        // Arrange 
        var applicationUser = _fixture.UserAdmin;

        _accountManagerServiceMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);
        _accountManagerServiceMock.Setup(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:ChangePasswordQueueName", "ha.queue.mock_queuename");

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value.Should().BeTrue();

        _accountManagerServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<ChangePasswordCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Once);
    }


    [Fact(DisplayName = "Deve lançar exceção se usuario aplicação não for encontrado")]
    public async Task Handle_ShouldThrowException_When_UserNotFound()
    {
        // Arrange 
        _accountManagerServiceMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(_request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Usuário com o e-mail '{_request.Email}' não encontrado");

        _accountManagerServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<ChangePasswordCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }


    [Fact(DisplayName = "Deve retornar erro se o reset falhar")]
    public async Task Handle_ShouldReturnError_WhenResetFails()
    {
        // Arrange 
        var applicationUser = _fixture.UserAdmin;
        _accountManagerServiceMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(applicationUser);
        _accountManagerServiceMock.Setup(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(_request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exception.Should().BeOfType<OperationErrorException>()
            .Which.Message.Should().Be("Falha ao resetar a senha do usuário.");

        _accountManagerServiceMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _accountManagerServiceMock.Verify(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<ChangePasswordCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Never);
    }
}