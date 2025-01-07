﻿using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithLocaisAtendimento;

public class SelectEspecialistaByIdWithLocaisAtendimentoRequestHandler : IRequestHandler<SelectEspecialistaByIdWithLocaisAtendimentoRequest, Result<EspecialistaResultList<EspecialistaLocalAtendimentoResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByIdWithLocaisAtendimentoRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResultList<EspecialistaLocalAtendimentoResponse>>> Handle(SelectEspecialistaByIdWithLocaisAtendimentoRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdWithLocaisAtendimentoAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Especialista com o ID: '{request.Id}' não encontrado");

        var response = new EspecialistaResultList<EspecialistaLocalAtendimentoResponse>(data.EspecialistaId, EspecialistaLocalAtendimentoResponse.MapFromEntity(data.LocaisAtendimento));

        return Result.Success<EspecialistaResultList<EspecialistaLocalAtendimentoResponse>>(response);
    }
}