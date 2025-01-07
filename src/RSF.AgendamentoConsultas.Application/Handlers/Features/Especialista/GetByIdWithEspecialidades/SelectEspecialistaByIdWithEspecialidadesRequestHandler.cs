﻿using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithEspecialidades;

public class SelectEspecialistaByIdWithEspecialidadesRequestHandler : IRequestHandler<SelectEspecialistaByIdWithEspecialidadesRequest, Result<EspecialistaResultList<EspecialistaEspecialidadeResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByIdWithEspecialidadesRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResultList<EspecialistaEspecialidadeResponse>>> Handle(SelectEspecialistaByIdWithEspecialidadesRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdWithEspecialidadesAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Especialista com o ID: '{request.Id}' não encontrado");

        var list = EspecialistaEspecialidadeResponse.MapFromEntity(data.Especialidades?.Select(c => c.Especialidade));

        var response = new EspecialistaResultList<EspecialistaEspecialidadeResponse>(data.EspecialistaId, list);

        return Result.Success<EspecialistaResultList<EspecialistaEspecialidadeResponse>>(response);
    }
}