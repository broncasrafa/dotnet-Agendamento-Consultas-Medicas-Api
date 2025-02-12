using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByEspecialista;

public class ConfirmAgendamentoByEspecialistaRequestHandler : IRequestHandler<ConfirmAgendamentoByEspecialistaRequest, Result<bool>>
{
    private readonly IAgendamentoConsultaRepository _agendamentoConsultaRepository;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;

    public ConfirmAgendamentoByEspecialistaRequestHandler(
        IAgendamentoConsultaRepository agendamentoConsultaRepository,
        IEventBus eventBus,
        IConfiguration configuration)
    {
        _agendamentoConsultaRepository = agendamentoConsultaRepository;
        _eventBus = eventBus;
        _configuration = configuration;
    }

    public async Task<Result<bool>> Handle(ConfirmAgendamentoByEspecialistaRequest request, CancellationToken cancellationToken)
    {
        var agendamento = await _agendamentoConsultaRepository.GetByIdAsync(request.AgendamentoId);
        NotFoundException.ThrowIfNull(agendamento, $"Agendamento com o ID: '{request.AgendamentoId}' não foi encontrado");

        var especialista = agendamento.Especialista;
        var isCurrentEspecialista = especialista?.EspecialistaId == request.EspecialistaId;
        NotFoundException.ThrowIfNull(!isCurrentEspecialista ? null : especialista, $"Especialista com o ID: '{request.EspecialistaId}' não pertence ao Agendamento com o ID: '{request.AgendamentoId}'");

        agendamento.ConfirmarProfissional();

        var rowsAffected = await _agendamentoConsultaRepository.UpdateAsync(agendamento);

        //_eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:AgendamentoQueueName").Value);

        return await Task.FromResult(rowsAffected > 0);
    }
}