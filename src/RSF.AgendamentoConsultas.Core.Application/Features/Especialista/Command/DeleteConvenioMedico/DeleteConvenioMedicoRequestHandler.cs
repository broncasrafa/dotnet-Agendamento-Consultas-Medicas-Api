using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteConvenioMedico;

public class DeleteConvenioMedicoRequestHandler : IRequestHandler<DeleteConvenioMedicoRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IConvenioMedicoRepository _convenioMedicoRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaConvenioMedico> _especialistaConvenioMedicoRepository;
    private readonly IHttpContextAccessor _httpContext;

    public DeleteConvenioMedicoRequestHandler(
        IEspecialistaRepository especialistaRepository,
        IConvenioMedicoRepository convenioMedicoRepository,
        IBaseRepository<Domain.Entities.EspecialistaConvenioMedico> especialistaConvenioMedicoRepository,
        IHttpContextAccessor httpContext)
    {
        _especialistaRepository = especialistaRepository;
        _convenioMedicoRepository = convenioMedicoRepository;
        _especialistaConvenioMedicoRepository = especialistaConvenioMedicoRepository;
        _httpContext = httpContext;
    }

    public async Task<Result<bool>> Handle(DeleteConvenioMedicoRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.EspecialistaId, ETipoPerfilAcesso.Profissional);

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