﻿using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetAll;

public class SelectRegiaoRequestHandler : IRequestHandler<SelectRegiaoRequest, Result<IReadOnlyList<RegiaoResponse>>>
{
    private readonly IRegiaoRepository _repository;

    public SelectRegiaoRequestHandler(IRegiaoRepository regiaoRepository) => _repository = regiaoRepository;

    public async Task<Result<IReadOnlyList<RegiaoResponse>>> Handle(SelectRegiaoRequest request, CancellationToken cancellationToken)
    {
        var regioes = await _repository.GetAllAsync();

        return Result.Success<IReadOnlyList<RegiaoResponse>>(RegiaoResponse.MapFromEntity(regioes));
    }
}
