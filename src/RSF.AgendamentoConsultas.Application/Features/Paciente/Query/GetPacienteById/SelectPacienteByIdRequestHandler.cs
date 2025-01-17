﻿using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteById;

public class SelectPacienteByIdRequestHandler : IRequestHandler<SelectPacienteByIdRequest, Result<PacienteResponse>>
{
    private readonly IPacienteRepository _repository;

    public SelectPacienteByIdRequestHandler(IPacienteRepository repository) => _repository = repository;

    public async Task<Result<PacienteResponse>> Handle(SelectPacienteByIdRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _repository.GetByIdDetailsAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");
        return await Task.FromResult(PacienteResponse.MapFromEntity(paciente));
    }
}