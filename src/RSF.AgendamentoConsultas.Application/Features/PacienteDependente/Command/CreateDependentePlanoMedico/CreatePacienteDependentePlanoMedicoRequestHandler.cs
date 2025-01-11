using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.CreateDependentePlanoMedico;

public class CreatePacienteDependentePlanoMedicoRequestHandler : IRequestHandler<CreatePacienteDependentePlanoMedicoRequest, Result<PacienteDependentePlanoMedicoResponse>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IConvenioMedicoRepository _convenioMedicoRepository;
    private readonly IBaseRepository<PacienteDependentePlanoMedico> _dependentePlanoMedicoRepository;

    public CreatePacienteDependentePlanoMedicoRequestHandler(
        IPacienteRepository pacienteRepository,
        IBaseRepository<PacienteDependentePlanoMedico> dependentePlanoMedicoRepository,
        IConvenioMedicoRepository convenioMedicoRepository)
    {
        _convenioMedicoRepository = convenioMedicoRepository;
        _dependentePlanoMedicoRepository = dependentePlanoMedicoRepository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<PacienteDependentePlanoMedicoResponse>> Handle(CreatePacienteDependentePlanoMedicoRequest request, CancellationToken cancellationToken)
    {
        var convenioMedico = await _convenioMedicoRepository.GetByIdAsync(request.ConvenioMedicoId);
        NotFoundException.ThrowIfNull(convenioMedico, $"Convênio Médico com o ID: '{request.ConvenioMedicoId}' não encontrado");

        var paciente = await _pacienteRepository.GetByIdDetailsAsync(request.PacientePrincipalId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente Principal com o ID: '{request.PacientePrincipalId}' não foi encontrado");

        var dependente = paciente.Dependentes.SingleOrDefault(c => c.DependenteId == request.DependenteId);
        NotFoundException.ThrowIfNull(dependente, $"Paciente Dependente com o ID: '{request.DependenteId}' não foi encontrado para o Paciente Principal com o ID: '{request.PacientePrincipalId}'");

        var planoMedico = new PacienteDependentePlanoMedico(request.NomePlano, request.NumCartao, request.DependenteId, request.ConvenioMedicoId);
        await _dependentePlanoMedicoRepository.AddAsync(planoMedico);
        await _dependentePlanoMedicoRepository.SaveChangesAsync();

        return await Task.FromResult(PacienteDependentePlanoMedicoResponse.MapFromEntity(planoMedico));
    }
}