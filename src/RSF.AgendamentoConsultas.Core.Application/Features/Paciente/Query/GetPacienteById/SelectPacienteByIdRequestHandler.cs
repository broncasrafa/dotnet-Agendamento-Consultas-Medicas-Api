using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteById;

public class SelectPacienteByIdRequestHandler : IRequestHandler<SelectPacienteByIdRequest, Result<PacienteResponse>>
{
    private readonly IPacienteRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public SelectPacienteByIdRequestHandler(IPacienteRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<Result<PacienteResponse>> Handle(SelectPacienteByIdRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacienteId, ETipoPerfilAcesso.Paciente);
        var paciente = await _repository.GetByIdDetailsAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");
        return await Task.FromResult(PacienteResponse.MapFromEntity(paciente));
    }
}