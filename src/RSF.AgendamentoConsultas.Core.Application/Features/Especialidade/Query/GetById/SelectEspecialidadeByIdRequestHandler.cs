﻿using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Query.GetById;

public class SelectEspecialidadeByIdRequestHandler : IRequestHandler<SelectEspecialidadeByIdRequest, Result<EspecialidadeResponse>>
{
    private readonly IEspecialidadeRepository _repository;

    public SelectEspecialidadeByIdRequestHandler(IEspecialidadeRepository repository) => _repository = repository;

    public async Task<Result<EspecialidadeResponse>> Handle(SelectEspecialidadeByIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.Id);
        NotFoundException.ThrowIfNull(data, $"Especialidade com o ID: '{request.Id}' não encontrada");
        return await Task.FromResult(EspecialidadeResponse.MapFromEntity(data));
    }
}