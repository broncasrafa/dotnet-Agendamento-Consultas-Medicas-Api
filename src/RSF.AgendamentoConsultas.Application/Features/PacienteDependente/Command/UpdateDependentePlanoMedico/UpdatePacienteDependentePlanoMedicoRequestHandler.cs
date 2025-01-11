using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.UpdateDependentePlanoMedico;

public class UpdatePacienteDependentePlanoMedicoRequestHandler : IRequestHandler<UpdatePacienteDependentePlanoMedicoRequest, Result<bool>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IConvenioMedicoRepository _convenioMedicoRepository;
    private readonly IBaseRepository<PacienteDependentePlanoMedico> _dependentePlanoMedicoRepository;

    public UpdatePacienteDependentePlanoMedicoRequestHandler(
        IBaseRepository<PacienteDependentePlanoMedico> dependentePlanoMedicoRepository,
        IPacienteRepository pacienteRepository,
        IConvenioMedicoRepository convenioMedicoRepository)
    {
        _dependentePlanoMedicoRepository = dependentePlanoMedicoRepository;
        _pacienteRepository = pacienteRepository;
        _convenioMedicoRepository = convenioMedicoRepository;
    }

    public async Task<Result<bool>> Handle(UpdatePacienteDependentePlanoMedicoRequest request, CancellationToken cancellationToken)
    {
        var convenioMedico = await _convenioMedicoRepository.GetByIdAsync(request.ConvenioMedicoId);
        NotFoundException.ThrowIfNull(convenioMedico, $"Convênio Médico com o ID: '{request.ConvenioMedicoId}' não encontrado");

        var pacientePrincipal = await _pacienteRepository.GetByIdDetailsAsync(request.PacientePrincipalId);
        NotFoundException.ThrowIfNull(pacientePrincipal, $"Paciente principal com o ID: '{request.PacientePrincipalId}' não foi encontrado");

        var dependente = pacientePrincipal.Dependentes.SingleOrDefault(c => c.DependenteId == request.DependenteId);
        NotFoundException.ThrowIfNull(dependente, $"Dependente com o ID: '{request.DependenteId}' não foi encontrado para o Paciente principal ID: '{request.PacientePrincipalId}'");

        var planoMedico = dependente!.PlanosMedicos.SingleOrDefault(c => c.PlanoMedicoId == request.PlanoMedicoId);
        NotFoundException.ThrowIfNull(planoMedico, $"Plano Médico com o ID: '{request.PlanoMedicoId}' não foi encontrado para o Dependente ID: '{request.DependenteId}'");

        planoMedico!.Update(request.NomePlano, request.NumeroCarteirinha, request.ConvenioMedicoId);

        var rowsAffected = await _dependentePlanoMedicoRepository.UpdateAsync(planoMedico);

        return await Task.FromResult(rowsAffected > 0);
    }
}