using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdDependentes;

public class SelectPacienteDependentesRequestHandler : IRequestHandler<SelectPacienteDependentesRequest, Result<PacienteResultList<PacienteDependenteResponse>>>
{
    private readonly IPacienteRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public SelectPacienteDependentesRequestHandler(IPacienteRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<Result<PacienteResultList<PacienteDependenteResponse>>> Handle(SelectPacienteDependentesRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacienteId, ETipoPerfilAcesso.Paciente);

        var dependentes = await _repository.GetDependentesPacienteByIdAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(dependentes.Count == 0 ? null : dependentes, $"Paciente Principal com o ID: '{request.PacienteId}' não foi encontrado");

        var response = new PacienteResultList<PacienteDependenteResponse>(request.PacienteId, PacienteDependenteResponse.MapFromEntity(dependentes));

        return Result.Success(response);
    }
}

