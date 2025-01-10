﻿using RSF.AgendamentoConsultas.Application.Handlers.Features.Agendamento.Responses;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Agendamento.Query.GetAgendamentoById;

public class SelectAgendamentoByIdRequestHandler : IRequestHandler<SelectAgendamentoByIdRequest, Result<AgendamentoResponse>>
{
    private readonly IAgendamentoConsultaRepository _agendamentoConsultaRepository;

    public SelectAgendamentoByIdRequestHandler(IAgendamentoConsultaRepository agendamentoConsultaRepository) 
        => _agendamentoConsultaRepository = agendamentoConsultaRepository;

    public async Task<Result<AgendamentoResponse>> Handle(SelectAgendamentoByIdRequest request, CancellationToken cancellationToken)
    {
        var agendamento = await _agendamentoConsultaRepository.GetByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(agendamento, $"Agendamento com o ID: '{request.Id}' não encontrado");

        return await Task.FromResult(AgendamentoResponse.MapFromEntity(agendamento));
    }
}