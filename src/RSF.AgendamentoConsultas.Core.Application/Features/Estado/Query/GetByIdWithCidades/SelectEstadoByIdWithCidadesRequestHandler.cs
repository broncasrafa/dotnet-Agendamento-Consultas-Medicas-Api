﻿using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Estado.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Estado.Query.GetByIdWithCidades;

public class SelectEstadoByIdWithCidadesRequestHandler : IRequestHandler<SelectEstadoByIdWithCidadesRequest, Result<EstadoResponse>>
{
    private readonly IEstadoRepository _repository;

    public SelectEstadoByIdWithCidadesRequestHandler(IEstadoRepository EstadoRepository) => _repository = EstadoRepository;

    public async Task<Result<EstadoResponse>> Handle(SelectEstadoByIdWithCidadesRequest request, CancellationToken cancellationToken)
    {
        var estados = await _repository.GetByIdWithCidadesAsync(request.Id);
        NotFoundException.ThrowIfNull(estados, $"Estado com o ID: '{request.Id}' não encontrado");
        return await Task.FromResult(EstadoResponse.MapFromEntity(estados));
    }
}