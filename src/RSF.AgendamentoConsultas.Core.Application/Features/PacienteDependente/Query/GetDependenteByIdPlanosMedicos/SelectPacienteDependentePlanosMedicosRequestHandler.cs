using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;

public class SelectPacienteDependentePlanosMedicosRequestHandler : IRequestHandler<SelectPacienteDependentePlanosMedicosRequest, Result<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>>
{
    private readonly IPacienteDependenteRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public SelectPacienteDependentePlanosMedicosRequestHandler(IPacienteDependenteRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<Result<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>> Handle(SelectPacienteDependentePlanosMedicosRequest request, CancellationToken cancellationToken)
    {
        var dependente = await _repository.GetByIdAsync(request.DependenteId);
        NotFoundException.ThrowIfNull(dependente, $"Paciente Dependente com o ID: '{request.DependenteId}' não foi encontrado");

        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, dependente.PacientePrincipalId, ETipoPerfilAcesso.Paciente);

        var response = new PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>(
            request.DependenteId,
            dependente.PacientePrincipalId,
            PacienteDependentePlanoMedicoResponse.MapFromEntity(dependente.PlanosMedicos));

        return Result.Success(response);
    }
}