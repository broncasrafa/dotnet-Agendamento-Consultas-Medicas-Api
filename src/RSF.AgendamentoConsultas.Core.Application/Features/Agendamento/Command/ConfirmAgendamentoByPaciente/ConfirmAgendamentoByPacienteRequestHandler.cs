using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByPaciente;

public class ConfirmAgendamentoByPacienteRequestHandler : IRequestHandler<ConfirmAgendamentoByPacienteRequest, Result<bool>>
{
    private readonly IAgendamentoConsultaRepository _agendamentoConsultaRepository;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;

    public ConfirmAgendamentoByPacienteRequestHandler(
        IAgendamentoConsultaRepository agendamentoConsultaRepository, 
        IEventBus eventBus, 
        IConfiguration configuration)
    {
        _agendamentoConsultaRepository = agendamentoConsultaRepository;
        _eventBus = eventBus;
        _configuration = configuration;
    }

    public async Task<Result<bool>> Handle(ConfirmAgendamentoByPacienteRequest request, CancellationToken cancellationToken)
    {
        var agendamento = await _agendamentoConsultaRepository.GetByIdAsync(request.AgendamentoId);
        NotFoundException.ThrowIfNull(agendamento, $"Agendamento com o ID: '{request.AgendamentoId}' não foi encontrado");

        var paciente = agendamento.Paciente;
        var isCurrentPaciente = paciente?.PacienteId == request.PacienteId;
        NotFoundException.ThrowIfNull(!isCurrentPaciente ? null : paciente, $"Paciente com o ID: '{request.PacienteId}' não pertence ao Agendamento com o ID: '{request.AgendamentoId}'");

        agendamento.ConfirmarPaciente();

        var rowsAffected = await _agendamentoConsultaRepository.UpdateAsync(agendamento);

        //_eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:AgendamentoQueueName").Value);

        return await Task.FromResult(rowsAffected > 0);
    }
}