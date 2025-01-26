using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteConvenioMedico;

public class DeleteConvenioMedicoRequestHandler : IRequestHandler<DeleteConvenioMedicoRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IConvenioMedicoRepository _convenioMedicoRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaConvenioMedico> _especialistaConvenioMedicoRepository;

    public DeleteConvenioMedicoRequestHandler(
        IEspecialistaRepository especialistaRepository, 
        IConvenioMedicoRepository convenioMedicoRepository, 
        IBaseRepository<Domain.Entities.EspecialistaConvenioMedico> especialistaConvenioMedicoRepository)
    {
        _especialistaRepository = especialistaRepository;
        _convenioMedicoRepository = convenioMedicoRepository;
        _especialistaConvenioMedicoRepository = especialistaConvenioMedicoRepository;
    }

    public async Task<Result<bool>> Handle(DeleteConvenioMedicoRequest request, CancellationToken cancellationToken)
    {
        var convenioMedico = await _convenioMedicoRepository.GetByIdAsync(request.ConvenioMedicoId);
        NotFoundException.ThrowIfNull(convenioMedico, $"Convênio Médico com o ID: '{request.ConvenioMedicoId}' não encontrado");

        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var convenioAtendido = especialista.ConveniosMedicosAtendidos.FirstOrDefault(c => c.ConvenioMedicoId == request.ConvenioMedicoId);
        NotFoundException.ThrowIfNull(convenioAtendido,
            $"Convênio Médico com o ID: '{request.ConvenioMedicoId}' não encontrado para o Especialista com o ID: '{request.EspecialistaId}'");

        var rowsAffected = await _especialistaConvenioMedicoRepository.RemoveAsync(convenioAtendido);
        
        return await Task.FromResult(rowsAffected > 0);
    }
}