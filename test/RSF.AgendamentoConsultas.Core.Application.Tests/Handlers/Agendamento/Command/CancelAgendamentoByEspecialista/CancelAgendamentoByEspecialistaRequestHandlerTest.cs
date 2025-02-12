using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CancelAgendamentoByEspecialista;
using RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Fixture;
using Moq;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Command.CancelAgendamentoByEspecialista;

[Collection(nameof(AgendamentoConsultaTestFixture))]
public class CancelAgendamentoByEspecialistaRequestHandlerTest
{
    private readonly AgendamentoConsultaTestFixture _fixture;
    private readonly Mock<IAgendamentoConsultaRepository> _agendamentoConsultaRepositoryMock;
    private readonly Mock<IEspecialistaRepository> _especialistaRepositoryMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly CancelAgendamentoByEspecialistaRequestHandler _handler;

    public CancelAgendamentoByEspecialistaRequestHandlerTest(AgendamentoConsultaTestFixture fixture)
    {
        _fixture = fixture;
        _agendamentoConsultaRepositoryMock = new Mock<IAgendamentoConsultaRepository>();
        _especialistaRepositoryMock = new Mock<IEspecialistaRepository>();
        _eventBusMock = new Mock<IEventBus>();
        _handler = new CancelAgendamentoByEspecialistaRequestHandler(
            _agendamentoConsultaRepositoryMock.Object,
            _especialistaRepositoryMock.Object,
            _fixture.ConfigurationMock.Object,
            _eventBusMock.Object);
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Agendamento não encontrado")]
    public async Task Handle_ShouldThrowException_When_AgendamentoId_NotExists()
    {
        // Arrange
        var request = new CancelAgendamentoByEspecialistaRequest(1, 1);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Agendamento com o ID: '{request.AgendamentoId}' não foi encontrado");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Especialista não encontrado")]
    public async Task Handle_ShouldThrowException_When_EspecialistaId_NotExists()
    {
        // Arrange
        var request = new CancelAgendamentoByEspecialistaRequest(1, 1);
        var agendamento = _fixture.GetAgendamentoConsulta();
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(agendamento);
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Especialista com o ID: '{request.EspecialistaId}' não foi encontrado");
    }


    [Fact(DisplayName = "Deve cancelar o agendamento e publicar evento com sucesso")]
    public async Task Handle_ShouldCancelAndPublishEvent_When_RequestIsValid()
    {
        // Arrange
        var request = new CancelAgendamentoByEspecialistaRequest(1, 1);
        var agendamento = _fixture.GetAgendamentoConsulta();
        var especialista = new Especialista(1, Guid.NewGuid().ToString(), "Teste", "CRM 123456 SP", "teste@teste.com", "Masculino");

        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(agendamento);
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(especialista);
        _agendamentoConsultaRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<AgendamentoConsulta>())).ReturnsAsync(1);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:AgendamentoCanceladoEspecialistaQueueName", "ha.queue.mock_queuename");

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value.Should().BeTrue();
        agendamento.StatusConsultaId.Should().Equals((int)ETipoStatusConsulta.Cancelado);
        agendamento.UpdatedAt.Should().NotBeNull();

        _agendamentoConsultaRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _especialistaRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _agendamentoConsultaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<AgendamentoConsulta>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<AgendamentoCanceledByEspecialistaEvent>(), "ha.queue.mock_queuename", ""), Times.Once);
    }
}