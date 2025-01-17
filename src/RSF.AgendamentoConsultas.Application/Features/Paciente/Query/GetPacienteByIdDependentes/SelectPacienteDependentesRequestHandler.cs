﻿using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteByIdDependentes;

public class SelectPacienteDependentesRequestHandler : IRequestHandler<SelectPacienteDependentesRequest, Result<PacienteResultList<PacienteDependenteResponse>>>
{
    private readonly IPacienteRepository _repository;

    public SelectPacienteDependentesRequestHandler(IPacienteRepository repository) => _repository = repository;


    public async Task<Result<PacienteResultList<PacienteDependenteResponse>>> Handle(SelectPacienteDependentesRequest request, CancellationToken cancellationToken)
    {
        var dependentes = await _repository.GetDependentesPacienteByIdAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(dependentes.Count == 0 ? null : dependentes, $"Paciente Principal com o ID: '{request.PacienteId}' não foi encontrado");

        var response = new PacienteResultList<PacienteDependenteResponse>(request.PacienteId, PacienteDependenteResponse.MapFromEntity(dependentes));

        return Result.Success(response);
    }
}

