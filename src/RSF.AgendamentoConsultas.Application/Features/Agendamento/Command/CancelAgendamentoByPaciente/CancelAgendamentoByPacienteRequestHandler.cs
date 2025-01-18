using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.CancelAgendamentoByPaciente;

public class CancelAgendamentoByPacienteRequestHandler : IRequestHandler<CancelAgendamentoByPacienteRequest, Result<bool>>
{
    private readonly IAgendamentoConsultaRepository _agendamentoConsultaRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IConfiguration _configuration;
    private readonly IEventBus _eventBus;

    public CancelAgendamentoByPacienteRequestHandler(
        IAgendamentoConsultaRepository agendamentoConsultaRepository, 
        IPacienteRepository pacienteRepository, 
        IConfiguration configuration, 
        IEventBus eventBus)
    {
        _agendamentoConsultaRepository = agendamentoConsultaRepository;
        _pacienteRepository = pacienteRepository;
        _configuration = configuration;
        _eventBus = eventBus;
    }

    public async Task<Result<bool>> Handle(CancelAgendamentoByPacienteRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _pacienteRepository.GetByIdDetailsAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        Domain.Entities.AgendamentoConsulta agendamento;

        if (request.DependenteId is not null)
        {
            var dependente = paciente.Dependentes.SingleOrDefault(c => c.DependenteId == request.DependenteId)!;
            NotFoundException.ThrowIfNull(dependente, $"Dependente com o ID: '{request.DependenteId}' não encontrado para o paciente ID: '{request.PacienteId}'");

            agendamento = await _agendamentoConsultaRepository.GetByIdAsync(request.AgendamentoId, request.PacienteId, request.DependenteId.Value);
            NotFoundException.ThrowIfNull(agendamento, $"Agendamento com o ID: '{request.AgendamentoId}' não encontrado para o Paciente Dependente com o ID: '{request.DependenteId}'");
        }
        else
        {
            agendamento = await _agendamentoConsultaRepository.GetByIdAsync(request.AgendamentoId, request.PacienteId);
            NotFoundException.ThrowIfNull(agendamento, $"Agendamento com o ID: '{request.AgendamentoId}' não encontrado para o Paciente com o ID: '{request.PacienteId}'");
        }

        agendamento.Cancelar(request.MotivoCancelamento);

        var rowsAffected = await _agendamentoConsultaRepository.UpdateAsync(agendamento);

        if (rowsAffected > 0)
        {
            var @event = new AgendamentoCanceledByPacienteEvent(
                paciente.Nome,
                paciente.Email,
                agendamento.Especialista.Nome,
                agendamento.Especialidade.NomePlural,
                agendamento.DataConsulta.ToShortDateString(),
                agendamento.HorarioConsulta,
                agendamento.LocalAtendimento.Nome,
                agendamento.NotaCancelamento);

            _eventBus.Publish<AgendamentoCanceledByPacienteEvent>(@event, _configuration.GetSection("RabbitMQ:AgendamentoCanceladoPacienteQueueName").Value);
        }           

        return await Task.FromResult(rowsAffected > 0);
    }
}