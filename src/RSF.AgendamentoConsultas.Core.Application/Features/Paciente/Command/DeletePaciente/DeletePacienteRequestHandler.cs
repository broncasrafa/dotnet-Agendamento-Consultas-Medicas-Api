using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePaciente;

public class DeletePacienteRequestHandler : IRequestHandler<DeletePacienteRequest, Result<bool>>
{
    private readonly IPacienteRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public DeletePacienteRequestHandler(IPacienteRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<Result<bool>> Handle(DeletePacienteRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacienteId, ETipoPerfilAcesso.Paciente);

        var paciente = await _repository.GetByIdAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        paciente.ChangeStatus(status: false);

        var rowsAffected = await _repository.ChangeStatusAsync(paciente);

        return await Task.FromResult(rowsAffected > 0);
    }
}