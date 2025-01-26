using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateConvenioMedico;

public class UpdateConvenioMedicoRequestHandler : IRequestHandler<UpdateConvenioMedicoRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IConvenioMedicoRepository _convenioMedicoRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaConvenioMedico> _especialistaConvenioMedicoRepository;

    public UpdateConvenioMedicoRequestHandler(
        IEspecialistaRepository especialistaRepository,
        IConvenioMedicoRepository convenioMedicoRepository,
        IBaseRepository<Domain.Entities.EspecialistaConvenioMedico> especialistaConvenioMedicoRepository)
    {
        _especialistaRepository = especialistaRepository;
        _convenioMedicoRepository = convenioMedicoRepository;
        _especialistaConvenioMedicoRepository = especialistaConvenioMedicoRepository;
    }

    public async Task<Result<bool>> Handle(UpdateConvenioMedicoRequest request, CancellationToken cancellationToken)
    {
        var convenioMedico = await _convenioMedicoRepository.GetByIdAsync(request.ConvenioMedicoId);
        NotFoundException.ThrowIfNull(convenioMedico, $"Convênio Médico com o ID: '{request.ConvenioMedicoId}' não encontrado");

        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var convenioAtendido = especialista.ConveniosMedicosAtendidos.FirstOrDefault(c => c.Id == request.Id);
        NotFoundException.ThrowIfNull(convenioAtendido,
            $"Convênio Médico com o ID de registro: '{request.Id}' não encontrado para o Especialista com o ID: '{request.EspecialistaId}'");

        convenioAtendido!.Update(request.ConvenioMedicoId);

        var rowsAffected = await _especialistaConvenioMedicoRepository.UpdateAsync(convenioAtendido);

        return await Task.FromResult(rowsAffected > 0);
    }
}