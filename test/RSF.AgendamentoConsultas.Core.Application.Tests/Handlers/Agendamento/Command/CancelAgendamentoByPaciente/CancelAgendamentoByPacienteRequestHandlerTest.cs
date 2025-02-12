using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Fixture;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CancelAgendamentoByPaciente;
using Moq;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Command.CancelAgendamentoByPaciente;

[Collection(nameof(AgendamentoConsultaTestFixture))]
public class CancelAgendamentoByPacienteRequestHandlerTest
{
    private readonly AgendamentoConsultaTestFixture _fixture;
    private readonly Mock<IAgendamentoConsultaRepository> _agendamentoConsultaRepositoryMock;
    private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly CancelAgendamentoByPacienteRequestHandler _handler;

    public CancelAgendamentoByPacienteRequestHandlerTest(AgendamentoConsultaTestFixture fixture)
    {
        _fixture = fixture;
        _agendamentoConsultaRepositoryMock = new Mock<IAgendamentoConsultaRepository>();
        _pacienteRepositoryMock = new Mock<IPacienteRepository>();
        _eventBusMock = new Mock<IEventBus>();
        _handler = new CancelAgendamentoByPacienteRequestHandler(
            _agendamentoConsultaRepositoryMock.Object,
            _pacienteRepositoryMock.Object,
            _fixture.ConfigurationMock.Object,
            _eventBusMock.Object);
    }


    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Paciente não encontrado")]
    public async Task Handle_ShouldThrowException_When_PacienteId_NotExists()
    {
        // Arrange
        var request = new CancelAgendamentoByPacienteRequest(1, 1, null, _fixture.Faker.Lorem.Text());
        _pacienteRepositoryMock.Setup(x => x.GetByIdDetailsAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Paciente com o ID: '{request.PacienteId}' não foi encontrado");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Agendamento de Paciente sem dependente não encontrado")]
    public async Task Handle_ShouldThrowException_When_AgendamentoId_NotExistsForPacienteWithoutDependentes()
    {
        // Arrange
        var request = new CancelAgendamentoByPacienteRequest(1, 1, null, _fixture.Faker.Lorem.Text());
        var paciente = _fixture.Paciente;
        _pacienteRepositoryMock.Setup(x => x.GetByIdDetailsAsync(It.IsAny<int>())).ReturnsAsync(paciente);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Agendamento com o ID: '{request.AgendamentoId}' não encontrado para o Paciente com o ID: '{request.PacienteId}'");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Dependente de Paciente não encontrado")]
    public async Task Handle_ShouldThrowException_When_DependenteId_NotExistsForPacienteWithDependentes()
    {
        // Arrange
        var request = new CancelAgendamentoByPacienteRequest(1, 1, 1, _fixture.Faker.Lorem.Text());
        var paciente = _fixture.Paciente;
        paciente.Dependentes = new List<PacienteDependente>();
        _pacienteRepositoryMock.Setup(x => x.GetByIdDetailsAsync(It.IsAny<int>())).ReturnsAsync(paciente);
        
        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Dependente com o ID: '{request.DependenteId}' não encontrado para o paciente ID: '{request.PacienteId}'");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Agendamento de Paciente Dependente não encontrado")]
    public async Task Handle_ShouldThrowException_When_AgendamentoId_NotExistsForPacienteWithDependentes()
    {
        // Arrange
        var request = new CancelAgendamentoByPacienteRequest(1, 1, 1, _fixture.Faker.Lorem.Text());
        var paciente = _fixture.Paciente;
        paciente.Dependentes = new List<PacienteDependente>
        {
            new PacienteDependente(1, "Nome", "32687248843", "teste@teste.com", "11965412454", "Feminino", "1990-11-04")
            {
                DependenteId = 1 ,
                PlanosMedicos = new List<PacienteDependentePlanoMedico>()
            }
        };
        _pacienteRepositoryMock.Setup(x => x.GetByIdDetailsAsync(It.IsAny<int>())).ReturnsAsync(paciente);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Agendamento com o ID: '{request.AgendamentoId}' não encontrado para o Paciente Dependente com o ID: '{request.DependenteId}'");
    }

    [Fact(DisplayName = "Deve cancelar o agendamento e publicar evento com sucesso")]
    public async Task Handle_ShouldCancelAndPublishEvent_When_RequestIsValid()
    {
        // Arrange
        var request = new CancelAgendamentoByPacienteRequest(1, 1, null, _fixture.Faker.Lorem.Text());
        var agendamento = _fixture.GetAgendamentoConsulta();
        var paciente = _fixture.Paciente;

        _pacienteRepositoryMock.Setup(x => x.GetByIdDetailsAsync(It.IsAny<int>())).ReturnsAsync(paciente);
        _agendamentoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(agendamento);
        _agendamentoConsultaRepositoryMock.Setup(c => c.UpdateAsync(It.IsAny<AgendamentoConsulta>())).ReturnsAsync(1);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:AgendamentoCanceladoPacienteQueueName", "ha.queue.mock_queuename");

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value.Should().BeTrue();
        agendamento.StatusConsultaId.Should().Equals((int)ETipoStatusConsulta.Cancelado);
        agendamento.UpdatedAt.Should().NotBeNull();

        _pacienteRepositoryMock.Verify(x => x.GetByIdDetailsAsync(It.IsAny<int>()), Times.Once);
        _agendamentoConsultaRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once);        
        _agendamentoConsultaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<AgendamentoConsulta>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<AgendamentoCanceledByPacienteEvent>(), "ha.queue.mock_queuename", ""), Times.Once);
    }
}