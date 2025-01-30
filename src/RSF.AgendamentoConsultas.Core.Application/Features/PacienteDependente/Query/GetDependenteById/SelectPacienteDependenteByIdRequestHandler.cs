using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteById;

public class SelectPacienteDependenteByIdRequestHandler : IRequestHandler<SelectPacienteDependenteByIdRequest, Result<PacienteDependenteResponse>>
{
    private readonly IPacienteDependenteRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public SelectPacienteDependenteByIdRequestHandler(IPacienteDependenteRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<Result<PacienteDependenteResponse>> Handle(SelectPacienteDependenteByIdRequest request, CancellationToken cancellationToken)
    {
        var dependente = await _repository.GetByIdAsync(request.DependenteId);
        NotFoundException.ThrowIfNull(dependente, $"Dependente com o ID: '{request.DependenteId}' não foi encontrado");

        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, dependente.PacientePrincipalId, ETipoPerfilAcesso.Paciente);

        return await Task.FromResult(PacienteDependenteResponse.MapFromEntity(dependente));
    }
}