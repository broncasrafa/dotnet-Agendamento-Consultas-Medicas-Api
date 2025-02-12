using RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Fixture;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByEspecialista;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using Moq;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Command.ConfirmAgendamentoByEspecialista;

[Collection(nameof(AgendamentoConsultaTestFixture))]
public class ConfirmAgendamentoByEspecialistaRequestHandlerTest
{
    private readonly AgendamentoConsultaTestFixture _fixture;
    private readonly Mock<IAgendamentoConsultaRepository> _agendamentoConsultaRepositoryMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly ConfirmAgendamentoByEspecialistaRequestHandler _handler;

    public ConfirmAgendamentoByEspecialistaRequestHandlerTest(AgendamentoConsultaTestFixture fixture)
    {
        _fixture = fixture;
        _agendamentoConsultaRepositoryMock = new Mock<IAgendamentoConsultaRepository>();
        _eventBusMock = new Mock<IEventBus>();
        _handler = new ConfirmAgendamentoByEspecialistaRequestHandler(_agendamentoConsultaRepositoryMock.Object, _eventBusMock.Object, _fixture.ConfigurationMock.Object);
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Agendamento não encontrado")]
    public async Task Handle_ShouldThrowException_When_AgendamentoId_NotExists()
    {
        // Arrange
        var request = new ConfirmAgendamentoByEspecialistaRequest(1, 1);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Agendamento com o ID: '{request.AgendamentoId}' não foi encontrado");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Especialista não encontrado para id diferente do id da request")]
    public async Task Handle_ShouldThrowException_When_EspecialistaId_IsDiferentFromRequest()
    {
        // Arrange
        var agendamento = _fixture.GetAgendamentoConsulta();
        var request = new ConfirmAgendamentoByEspecialistaRequest(1, 2);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(agendamento);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Especialista com o ID: '{request.EspecialistaId}' não pertence ao Agendamento com o ID: '{request.AgendamentoId}'");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Especialista não existente no agendamento")]
    public async Task Handle_ShouldThrowException_When_EspecialistaId_NotExists()
    {
        // Arrange
        var agendamento = _fixture.GetAgendamentoConsulta();
        agendamento.Especialista = null;
        var request = new ConfirmAgendamentoByEspecialistaRequest(1, 1);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(agendamento);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Especialista com o ID: '{request.EspecialistaId}' não pertence ao Agendamento com o ID: '{request.AgendamentoId}'");
    }

    [Fact(DisplayName = "Deve confirmar o agendamento com sucesso")]
    public async Task Handle_ShouldConfirm_When_RequestIsValid()
    {
        // Arrange
        var agendamento = _fixture.GetAgendamentoConsulta();
        var request = new ConfirmAgendamentoByEspecialistaRequest(1, 1);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(agendamento);
        _agendamentoConsultaRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<AgendamentoConsulta>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value.Should().BeTrue();
        agendamento.ConfirmedByEspecialistaAt.Should().NotBeNull();
        agendamento.UpdatedAt.Should().NotBeNull();
        
        _agendamentoConsultaRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _agendamentoConsultaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<AgendamentoConsulta>()), Times.Once);
    }
}