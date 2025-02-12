using RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Fixture;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CreateAgendamento;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using Moq;
using FluentAssertions;

namespace RSF.AgendamentoConsultas.Core.Application.Tests.Handlers.Agendamento.Command.CreateAgendamento;

[Collection(nameof(AgendamentoConsultaTestFixture))]
public class CreateAgendamentoRequestHandlerTest
{
    private readonly AgendamentoConsultaTestFixture _fixture;
    private readonly CreateAgendamentoRequestHandler _handler;
    private readonly CreateAgendamentoRequest _request;
    private readonly Mock<IAgendamentoConsultaRepository> _agendamentoConsultaRepositoryMock;
    private readonly Mock<IEspecialistaRepository> _especialistaRepositoryMock;
    private readonly Mock<IPacienteRepository> _pacienteRepositoryMock;
    private readonly Mock<IBaseRepository<Domain.Entities.TipoConsulta>> _tipoConsultaRepositoryMock;
    private readonly Mock<IBaseRepository<Domain.Entities.TipoAgendamento>> _tipoAgendamentoRepositoryMock;
    private readonly Mock<IEventBus> _eventBusMock;

    private readonly Especialista _especialista;

    public CreateAgendamentoRequestHandlerTest(AgendamentoConsultaTestFixture fixture)
    {
        _fixture = fixture;
        _agendamentoConsultaRepositoryMock = new Mock<IAgendamentoConsultaRepository>();
        _especialistaRepositoryMock = new Mock<IEspecialistaRepository>();
        _pacienteRepositoryMock = new Mock<IPacienteRepository>();
        _tipoConsultaRepositoryMock = new Mock<IBaseRepository<Domain.Entities.TipoConsulta>>();
        _tipoAgendamentoRepositoryMock = new Mock<IBaseRepository<Domain.Entities.TipoAgendamento>>();
        _eventBusMock = new Mock<IEventBus>();

        _handler = new CreateAgendamentoRequestHandler(
            _agendamentoConsultaRepositoryMock.Object,
            _especialistaRepositoryMock.Object,
            _tipoConsultaRepositoryMock.Object,
            _tipoAgendamentoRepositoryMock.Object,
            _pacienteRepositoryMock.Object,
            _eventBusMock.Object,
            _fixture.ConfigurationMock.Object);

        _request = new CreateAgendamentoRequest(
            EspecialistaId: 1,
            EspecialidadeId: 1,
            LocalAtendimentoId: 1,
            TipoConsultaId: 1,
            TipoAgendamentoId: 1,
            DataConsulta: DateTime.Now.AddDays(5),
            HorarioConsulta: "15:30",
            MotivoConsulta: "Teste",
            ValorConsulta: null,
            TelefoneContato: "11965451231",
            PrimeiraVez: false,
            PacienteId: 1,
            DependenteId: null,
            PlanoMedicoId: 1);

        _especialista = new Especialista(Guid.NewGuid().ToString(), "Teste", "CRM 1234 SP", "teste@teste.com", "Masculino", "Premium")
        {
            EspecialistaId = 1,
            Especialidades = new List<EspecialistaEspecialidade> { new EspecialistaEspecialidade(1, 1, "Principal"), new EspecialistaEspecialidade(1, 2, "SubEspecialidade") },
            LocaisAtendimento = new List<EspecialistaLocalAtendimento>
            {
                new EspecialistaLocalAtendimento(1, 1, "Local", "Rua dos Testes", "Complemento", "Bairro", "09945000", "Sao Paulo", "SP", 500M, "Tipo", "1154320987", "11978643637")
            }
        };
    }


    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Especialista não encontrado")]
    public async Task Handle_ShouldThrowException_When_Especialista_NotExists()
    {
        // Arrange
        var request = _request;
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Especialista com o ID: '{request.EspecialistaId}' não encontrado");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Especialidade não encontrado para o especialista")]
    public async Task Handle_ShouldThrowException_When_Especialidade_NotExistsForEspecialista()
    {
        // Arrange
        var request = _request;
        var especialista = new Especialista(Guid.NewGuid().ToString(), "Teste", "CRM 1234 SP", "teste@teste.com", "Masculino", "Premium")
        {
            EspecialistaId = 1,
            Especialidades = new List<EspecialistaEspecialidade>()
        };
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(especialista);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Especialidade com o ID: '{request.EspecialidadeId}' não encontrado para o especialista ID: '{request.EspecialistaId}'");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para LocaisAtendimento não encontrado para o especialista")]
    public async Task Handle_ShouldThrowException_When_LocaisAtendimento_NotExistsForEspecialista()
    {
        // Arrange
        var request = _request;
        var especialista = new Especialista(Guid.NewGuid().ToString(), "Teste", "CRM 1234 SP", "teste@teste.com", "Masculino", "Premium")
        {
            EspecialistaId = 1,
            Especialidades = new List<EspecialistaEspecialidade> { new EspecialistaEspecialidade(1, 1, "Principal"), new EspecialistaEspecialidade(1, 2, "SubEspecialidade") },
            LocaisAtendimento = new List<EspecialistaLocalAtendimento>()
        };

        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(especialista);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Local de atendimento com o ID: '{request.LocalAtendimentoId}' não encontrado para o especialista ID: '{request.EspecialistaId}'");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Tipo de Consulta não encontrado")]
    public async Task Handle_ShouldThrowException_When_TipoConsulta_NotExists()
    {
        // Arrange
        var request = _request;
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialista);
        _tipoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Tipo de Consulta com o ID: '{request.TipoConsultaId}' não encontrada");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Tipo de Agendamento não encontrado")]
    public async Task Handle_ShouldThrowException_When_TipoAgendamento_NotExists()
    {
        // Arrange
        var request = _request;
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialista);
        _tipoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoConsulta());
        _tipoAgendamentoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Tipo de Agendamento com o ID: '{request.TipoAgendamentoId}' não encontrado");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Paciente não encontrado")]
    public async Task Handle_ShouldThrowException_When_Paciente_NotExists()
    {
        // Arrange
        var request = _request;
        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialista);
        _tipoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoConsulta());
        _tipoAgendamentoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoAgendamento());
        _pacienteRepositoryMock.Setup(x => x.GetByIdDetailsAsync(It.IsAny<int>()));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Paciente com o ID: '{request.PacienteId}' não foi encontrado");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Planos Medicos do Paciente não encontrado")]
    public async Task Handle_ShouldThrowException_When_PlanosMedicosPaciente_NotExists()
    {
        // Arrange
        var request = _request;
        var paciente = _fixture.Paciente;
        paciente.PlanosMedicos = new List<PacientePlanoMedico>();

        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialista);
        _tipoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoConsulta());
        _tipoAgendamentoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoAgendamento());
        _pacienteRepositoryMock.Setup(x => x.GetByIdDetailsAsync(It.IsAny<int>())).ReturnsAsync(paciente);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Plano Medico com o ID: '{request.PlanoMedicoId}' do paciente ID: '{request.PacienteId}' não encontrado");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Dependente do Paciente não encontrado")]
    public async Task Handle_ShouldThrowException_When_DependentePaciente_NotExists()
    {
        // Arrange
        var request = new CreateAgendamentoRequest(
            EspecialistaId: 1,
            EspecialidadeId: 1,
            LocalAtendimentoId: 1,
            TipoConsultaId: 1,
            TipoAgendamentoId: 1,
            DataConsulta: DateTime.Now.AddDays(5),
            HorarioConsulta: "15:30",
            MotivoConsulta: "Teste",
            ValorConsulta: null,
            TelefoneContato: "11965451231",
            PrimeiraVez: false,
            PacienteId: 1,
            DependenteId: 1,
            PlanoMedicoId: 1);

        var paciente = _fixture.Paciente;
        paciente.Dependentes = new List<PacienteDependente>();

        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialista);
        _tipoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoConsulta());
        _tipoAgendamentoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoAgendamento());
        _pacienteRepositoryMock.Setup(x => x.GetByIdDetailsAsync(It.IsAny<int>())).ReturnsAsync(paciente);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Dependente com o ID: '{request.DependenteId}' não encontrado para o paciente ID: '{request.PacienteId}'");
    }

    [Fact(DisplayName = "Deve lançar a exceção NotFoundException para Planos Medicos do Dependente do Paciente não encontrado")]
    public async Task Handle_ShouldThrowException_When_DependentePacientePlanosMedicos_NotExists()
    {
        // Arrange
        var request = new CreateAgendamentoRequest(
            EspecialistaId: 1,
            EspecialidadeId: 1,
            LocalAtendimentoId: 1,
            TipoConsultaId: 1,
            TipoAgendamentoId: 1,
            DataConsulta: DateTime.Now.AddDays(5),
            HorarioConsulta: "15:30",
            MotivoConsulta: "Teste",
            ValorConsulta: null,
            TelefoneContato: "11965451231",
            PrimeiraVez: false,
            PacienteId: 1,
            DependenteId: 1,
            PlanoMedicoId: 1);

        var paciente = _fixture.Paciente;
        paciente.Dependentes = new List<PacienteDependente>
        {
            new PacienteDependente(1, "Nome", "32687248843", "teste@teste.com", "11965412454", "Feminino", "1990-11-04") 
            { 
                DependenteId = 1 ,
                PlanosMedicos = new List<PacienteDependentePlanoMedico>()
            } 
        };

        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialista);
        _tipoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoConsulta());
        _tipoAgendamentoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoAgendamento());
        _pacienteRepositoryMock.Setup(x => x.GetByIdDetailsAsync(It.IsAny<int>())).ReturnsAsync(paciente);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Plano Medico com o ID: '{request.PlanoMedicoId}' do dependente ID: '{request.DependenteId}' não encontrado");
    }


    [Fact(DisplayName = "Deve registrar o agendamento e publicar evento com sucesso")]
    public async Task Handle_ShouldRegisterAndPublishEvent_When_RequestIsValid()
    {
        // Arrange
        var request = _request;
        var paciente = _fixture.Paciente;
        paciente.PacienteId = 1;
        paciente.PlanosMedicos = new List<PacientePlanoMedico> { 
            new PacientePlanoMedico(1, "Nome", "123", 1, 1) { ConvenioMedico = new ConvenioMedico(1, "Teste") } };

        var newAgendamento = _fixture.GetAgendamentoConsulta();

        _especialistaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_especialista);
        _tipoConsultaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoConsulta());
        _tipoAgendamentoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new TipoAgendamento());
        _pacienteRepositoryMock.Setup(x => x.GetByIdDetailsAsync(It.IsAny<int>())).ReturnsAsync(paciente);

        _agendamentoConsultaRepositoryMock.Setup(c => c.AddAsync(It.IsAny<AgendamentoConsulta>())).ReturnsAsync(1);
        _agendamentoConsultaRepositoryMock.Setup(c => c.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(newAgendamento);
        _fixture.MockRabbitMqQueueConfiguration("RabbitMQ:AgendamentoQueueName", "ha.queue.mock_queuename");

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Exception.Should().BeNull();
        result.Value.Should().BeGreaterThan(0);

        _especialistaRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _tipoConsultaRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _tipoAgendamentoRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _pacienteRepositoryMock.Verify(x => x.GetByIdDetailsAsync(It.IsAny<int>()), Times.Once);
        _agendamentoConsultaRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AgendamentoConsulta>()), Times.Once);
        _agendamentoConsultaRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _eventBusMock.Verify(x => x.Publish(It.IsAny<AgendamentoCreatedEvent>(), "ha.queue.mock_queuename", ""), Times.Once);
    }
}

