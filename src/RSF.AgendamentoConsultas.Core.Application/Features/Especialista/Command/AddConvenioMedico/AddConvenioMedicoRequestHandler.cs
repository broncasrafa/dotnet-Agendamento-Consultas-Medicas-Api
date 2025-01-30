using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddConvenioMedico;

public class AddConvenioMedicoRequestHandler : IRequestHandler<AddConvenioMedicoRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IConvenioMedicoRepository _convenioMedicoRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaConvenioMedico> _especialistaConvenioMedicoRepository;
    private readonly IHttpContextAccessor _httpContext;

    public AddConvenioMedicoRequestHandler(
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

    public async Task<Result<bool>> Handle(AddConvenioMedicoRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.EspecialistaId, ETipoPerfilAcesso.Profissional);

        var convenioMedico = await _convenioMedicoRepository.GetByIdAsync(request.ConvenioMedicoId);
        NotFoundException.ThrowIfNull(convenioMedico, $"Convênio Médico com o ID: '{request.ConvenioMedicoId}' não encontrado");

        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado"); 
        
        AlreadyExistsException.ThrowIfExists(
            especialista.ConveniosMedicosAtendidos.Any(c => c.ConvenioMedicoId == request.ConvenioMedicoId), 
            $"Convênio Médico com o ID: '{request.ConvenioMedicoId}' já atendido pelo Especialista com o ID: '{request.EspecialistaId}'");

        var convenio = new Domain.Entities.EspecialistaConvenioMedico(request.EspecialistaId, request.ConvenioMedicoId);
        especialista.ConveniosMedicosAtendidos.Add(convenio);

        var rowsAffected = await _especialistaConvenioMedicoRepository.AddAsync(convenio);

        return await Task.FromResult(rowsAffected > 0);
    }
}