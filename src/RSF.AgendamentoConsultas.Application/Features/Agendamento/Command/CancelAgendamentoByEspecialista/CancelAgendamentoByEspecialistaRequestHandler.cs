using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.CancelAgendamentoByEspecialista;

public class CancelAgendamentoByEspecialistaRequestHandler : IRequestHandler<CancelAgendamentoByEspecialistaRequest, Result<bool>>
{
    private readonly IAgendamentoConsultaRepository _agendamentoConsultaRepository;
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IConfiguration _configuration;
    private readonly IEventBus _eventBus;

    public CancelAgendamentoByEspecialistaRequestHandler(
        IAgendamentoConsultaRepository agendamentoConsultaRepository, 
        IEspecialistaRepository especialistaRepository, 
        IConfiguration configuration, 
        IEventBus eventBus)
    {
        _agendamentoConsultaRepository = agendamentoConsultaRepository;
        _especialistaRepository = especialistaRepository;
        _configuration = configuration;
        _eventBus = eventBus;
    }

    public async Task<Result<bool>> Handle(CancelAgendamentoByEspecialistaRequest request, CancellationToken cancellationToken)
    {
        var agendamento = await _agendamentoConsultaRepository.GetByIdAsync(request.AgendamentoId);
        NotFoundException.ThrowIfNull(agendamento, $"Agendamento com o ID: '{request.AgendamentoId}' não foi encontrado");

        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não foi encontrado");

        agendamento.Cancelar($"Consulta cancelada pelo especialista {especialista.Nome}.");

        var rowsAffected = await _agendamentoConsultaRepository.UpdateAsync(agendamento);

        if (rowsAffected > 0)
        {
            var @event = new AgendamentoCanceledByEspecialistaEvent(
                agendamento.Paciente.Nome,
                agendamento.Paciente.Email,
                agendamento.Especialista.Nome,
                agendamento.Especialidade.NomePlural,
                agendamento.DataConsulta.ToShortDateString(),
                agendamento.HorarioConsulta,
                agendamento.LocalAtendimento.Nome);

            _eventBus.Publish<AgendamentoCanceledByEspecialistaEvent>(@event, _configuration.GetSection("RabbitMQ:AgendamentoCanceladoEspecialistaQueueName").Value);
        }

        return await Task.FromResult(rowsAffected > 0);
    }
}