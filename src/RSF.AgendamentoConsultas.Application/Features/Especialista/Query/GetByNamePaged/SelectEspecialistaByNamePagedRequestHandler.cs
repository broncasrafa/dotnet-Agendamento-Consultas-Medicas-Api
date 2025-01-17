﻿using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Results;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.Query.GetByNamePaged;

public class SelectEspecialistaByNamePagedRequestHandler : IRequestHandler<SelectEspecialistaByNamePagedRequest, Result<PagedResult<EspecialistaResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByNamePagedRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<PagedResult<EspecialistaResponse>>> Handle(SelectEspecialistaByNamePagedRequest request, CancellationToken cancellationToken)
    {
        var pagedResult = await _repository.GetAllByNamePagedAsync(request.Name, request.PageNum, request.PageSize);

        var response = EspecialistaResponse.MapFromEntityPaged(pagedResult, request.PageNum, request.PageSize);

        return Result.Success(response);
    }
}