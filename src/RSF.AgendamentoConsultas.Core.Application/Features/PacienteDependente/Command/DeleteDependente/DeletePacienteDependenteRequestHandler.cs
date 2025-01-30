using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.DeleteDependente;

public class DeletePacienteDependenteRequestHandler : IRequestHandler<DeletePacienteDependenteRequest, Result<bool>>
{
    private readonly IPacienteDependenteRepository _dependenteRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IHttpContextAccessor _httpContext;

    public DeletePacienteDependenteRequestHandler(
        IPacienteDependenteRepository dependenteRepository, 
        IPacienteRepository pacienteRepository, 
        IHttpContextAccessor httpContext)
    {
        _dependenteRepository = dependenteRepository;
        _pacienteRepository = pacienteRepository;
        _httpContext = httpContext;
    }

    public async Task<Result<bool>> Handle(DeletePacienteDependenteRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacientePrincipalId, ETipoPerfilAcesso.Paciente);

        var pacientePrincipal = await _pacienteRepository.GetByFilterAsync(c => c.PacienteId == request.PacientePrincipalId, c => c.Dependentes);
        NotFoundException.ThrowIfNull(pacientePrincipal, $"Paciente principal com o ID: '{request.PacientePrincipalId}' não foi encontrado");

        var dependente = pacientePrincipal.Dependentes.SingleOrDefault(c => c.DependenteId == request.DependenteId);
        NotFoundException.ThrowIfNull(dependente, $"Dependente com o ID: '{request.DependenteId}' não foi encontrado para o Paciente principal ID: '{request.PacientePrincipalId}'");

        dependente!.ChangeStatus(status: false);

        var rowsAffected = await _dependenteRepository.ChangeStatusAsync(dependente);

        return await Task.FromResult(rowsAffected > 0);
    }
}