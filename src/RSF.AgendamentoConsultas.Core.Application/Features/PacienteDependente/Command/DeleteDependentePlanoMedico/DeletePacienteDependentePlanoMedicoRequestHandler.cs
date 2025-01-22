using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.DeleteDependentePlanoMedico;

public class DeletePacienteDependentePlanoMedicoRequestHandler : IRequestHandler<DeletePacienteDependentePlanoMedicoRequest, Result<bool>>
{
    private readonly IBaseRepository<PacienteDependentePlanoMedico> _dependentePlanoMedicoRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public DeletePacienteDependentePlanoMedicoRequestHandler(IBaseRepository<PacienteDependentePlanoMedico> dependentePlanoMedicoRepository, IPacienteRepository pacienteRepository)
    {
        _dependentePlanoMedicoRepository = dependentePlanoMedicoRepository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<bool>> Handle(DeletePacienteDependentePlanoMedicoRequest request, CancellationToken cancellationToken)
    {
        var pacientePrincipal = await _pacienteRepository.GetByIdDetailsAsync(request.PacientePrincipalId);
        NotFoundException.ThrowIfNull(pacientePrincipal, $"Paciente principal com o ID: '{request.PacientePrincipalId}' não foi encontrado");

        var dependente = pacientePrincipal.Dependentes.SingleOrDefault(c => c.DependenteId == request.DependenteId);
        NotFoundException.ThrowIfNull(dependente, $"Dependente com o ID: '{request.DependenteId}' não foi encontrado para o Paciente principal ID: '{request.PacientePrincipalId}'");

        var planoMedico = dependente!.PlanosMedicos.SingleOrDefault(c => c.PlanoMedicoId == request.PlanoMedicoId);
        NotFoundException.ThrowIfNull(planoMedico, $"Plano Médico com o ID: '{request.PlanoMedicoId}' não foi encontrado para o Dependente ID: '{request.DependenteId}'");

        planoMedico!.ChangeStatus(status: false);

        var rowsAffected = await _dependentePlanoMedicoRepository.ChangeStatusAsync(planoMedico);

        return await Task.FromResult(rowsAffected > 0);
    }
}