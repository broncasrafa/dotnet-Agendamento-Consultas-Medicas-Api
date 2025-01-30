using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdPlanosMedicos;

public class SelectPacientePlanosMedicosRequestHandler : IRequestHandler<SelectPacientePlanosMedicosRequest, Result<PacienteResultList<PacientePlanoMedicoResponse>>>
{
    private readonly IPacienteRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public SelectPacientePlanosMedicosRequestHandler(IPacienteRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<Result<PacienteResultList<PacientePlanoMedicoResponse>>> Handle(SelectPacientePlanosMedicosRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacienteId, ETipoPerfilAcesso.Paciente);

        var planosMedicos = await _repository.GetPlanosMedicosPacienteByIdAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(planosMedicos.Count == 0 ? null : planosMedicos, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var response = new PacienteResultList<PacientePlanoMedicoResponse>(request.PacienteId, PacientePlanoMedicoResponse.MapFromEntity(planosMedicos));

        return Result.Success(response);
    }
}

