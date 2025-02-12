using RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Fixture;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByEspecialista;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using Moq;
using FluentAssertions;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByPaciente;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Command.ConfirmAgendamentoByPaciente;

[Collection(nameof(AgendamentoConsultaTestFixture))]
public class ConfirmAgendamentoByPacienteRequestHandlerTest
{
    private readonly AgendamentoConsultaTestFixture _fixture;
    private readonly Mock<IAgendamentoConsultaRepository> _agendamentoConsultaRepositoryMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly ConfirmAgendamentoByPacienteRequestHandler _handler;

    public ConfirmAgendamentoByPacienteRequestHandlerTest(AgendamentoConsultaTestFixture fixture)
    {
        _fixture = fixture;
        _agendamentoConsultaRepositoryMock = new Mock<IAgendamentoConsultaRepository>();
        _eventBusMock = new Mock<IEventBus>();
        _handler = new ConfirmAgendamentoByPacienteRequestHandler(_agendamentoConsultaRepositoryMock.Object, _eventBusMock.Object, _fixture.ConfigurationMock.Object);
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Agendamento não encontrado")]
    public async Task Handle_ShouldThrowException_When_AgendamentoId_NotExists()
    {
        // Arrange
        var request = new ConfirmAgendamentoByPacienteRequest(1, 1);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Agendamento com o ID: '{request.AgendamentoId}' não foi encontrado");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Paciente não encontrado para id diferente do id da request")]
    public async Task Handle_ShouldThrowException_When_PacienteId_IsDiferentFromRequest()
    {
        // Arrange
        var agendamento = _fixture.GetAgendamentoConsulta();
        var request = new ConfirmAgendamentoByPacienteRequest(1, 2);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(agendamento);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Paciente com o ID: '{request.PacienteId}' não pertence ao Agendamento com o ID: '{request.AgendamentoId}'");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Paciente não existente no agendamento")]
    public async Task Handle_ShouldThrowException_When_PacienteId_NotExists()
    {
        // Arrange
        var agendamento = _fixture.GetAgendamentoConsulta();
        agendamento.Paciente = null;
        var request = new ConfirmAgendamentoByPacienteRequest(1, 1);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(agendamento);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Paciente com o ID: '{request.PacienteId}' não pertence ao Agendamento com o ID: '{request.AgendamentoId}'");
    }

    [Fact(DisplayName = "Deve confirmar o agendamento com sucesso")]
    public async Task Handle_ShouldConfirm_When_RequestIsValid()
    {
        // Arrange
        var agendamento = _fixture.GetAgendamentoConsulta();
        agendamento.ConfirmedByEspecialistaAt = DateTime.Now;

        var request = new ConfirmAgendamentoByPacienteRequest(1, 1);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(agendamento);
        _agendamentoConsultaRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<AgendamentoConsulta>())).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value.Should().BeTrue();
        agendamento.ConfirmedByPacienteAt.Should().NotBeNull();
        agendamento.StatusConsultaId.Should().Equals((int)ETipoStatusConsulta.Confirmado);
        agendamento.UpdatedAt.Should().NotBeNull();

        _agendamentoConsultaRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _agendamentoConsultaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<AgendamentoConsulta>()), Times.Once);
    }
}