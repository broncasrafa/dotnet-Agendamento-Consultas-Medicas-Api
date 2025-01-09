using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Command.CreateDependentePlanoMedico;

public class CreatePacienteDependentePlanoMedicoRequestHandler : IRequestHandler<CreatePacienteDependentePlanoMedicoRequest, Result<PacienteDependentePlanoMedicoResponse>>
{
    private readonly IPacienteDependenteRepository _repository;
    private readonly IConvenioMedicoRepository _convenioMedicoRepository;
    private readonly IBaseRepository<PacienteDependentePlanoMedico> _dependentePlanoMedicoRepository;

    public CreatePacienteDependentePlanoMedicoRequestHandler(
        IPacienteDependenteRepository repository,
        IConvenioMedicoRepository convenioMedicoRepository,
        IBaseRepository<PacienteDependentePlanoMedico> dependentePlanoMedicoRepository)
    {
        _repository = repository;
        _convenioMedicoRepository = convenioMedicoRepository;
        _dependentePlanoMedicoRepository = dependentePlanoMedicoRepository;
    }

    public async Task<Result<PacienteDependentePlanoMedicoResponse>> Handle(CreatePacienteDependentePlanoMedicoRequest request, CancellationToken cancellationToken)
    {
        var convenioMedico = await _convenioMedicoRepository.GetByIdAsync(request.ConvenioMedicoId);
        NotFoundException.ThrowIfNull(convenioMedico, $"Convênio Médico com o ID: '{request.ConvenioMedicoId}' não encontrado");

        var paciente = await _repository.GetByIdAsync(request.DependenteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente Dependente com o ID: '{request.DependenteId}' não foi encontrado");

        var planoMedico = new PacienteDependentePlanoMedico(request.NomePlano, request.NumCartao, request.DependenteId, request.ConvenioMedicoId);
        await _dependentePlanoMedicoRepository.AddAsync(planoMedico);
        await _dependentePlanoMedicoRepository.SaveChangesAsync();

        return await Task.FromResult(PacienteDependentePlanoMedicoResponse.MapFromEntity(planoMedico));
    }
}