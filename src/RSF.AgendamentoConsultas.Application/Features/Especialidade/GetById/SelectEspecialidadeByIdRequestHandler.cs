﻿using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Especialidade.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialidade.GetById;

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