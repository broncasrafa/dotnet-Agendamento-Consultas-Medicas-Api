using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.CreateDependentePlanoMedico;

public class CreatePacienteDependentePlanoMedicoRequestHandler : IRequestHandler<CreatePacienteDependentePlanoMedicoRequest, Result<PacienteDependentePlanoMedicoResponse>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IConvenioMedicoRepository _convenioMedicoRepository;
    private readonly IBaseRepository<PacienteDependentePlanoMedico> _dependentePlanoMedicoRepository;
    private readonly IHttpContextAccessor _httpContext;

    public CreatePacienteDependentePlanoMedicoRequestHandler(
        IPacienteRepository pacienteRepository,
        IBaseRepository<PacienteDependentePlanoMedico> dependentePlanoMedicoRepository,
        IConvenioMedicoRepository convenioMedicoRepository,
        IHttpContextAccessor httpContext)
    {
        _convenioMedicoRepository = convenioMedicoRepository;
        _dependentePlanoMedicoRepository = dependentePlanoMedicoRepository;
        _pacienteRepository = pacienteRepository;
        _httpContext = httpContext;
    }

    public async Task<Result<PacienteDependentePlanoMedicoResponse>> Handle(CreatePacienteDependentePlanoMedicoRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacientePrincipalId, ETipoPerfilAcesso.Paciente);

        var convenioMedico = await _convenioMedicoRepository.GetByIdAsync(request.ConvenioMedicoId);
        NotFoundException.ThrowIfNull(convenioMedico, $"Convênio Médico com o ID: '{request.ConvenioMedicoId}' não encontrado");

        var paciente = await _pacienteRepository.GetByIdDetailsAsync(request.PacientePrincipalId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente Principal com o ID: '{request.PacientePrincipalId}' não foi encontrado");

        var dependente = paciente.Dependentes.SingleOrDefault(c => c.DependenteId == request.DependenteId);
        NotFoundException.ThrowIfNull(dependente, $"Paciente Dependente com o ID: '{request.DependenteId}' não foi encontrado para o Paciente Principal com o ID: '{request.PacientePrincipalId}'");

        var planoMedico = new PacienteDependentePlanoMedico(request.NomePlano, request.NumCartao, request.DependenteId, request.ConvenioMedicoId);
        
        await _dependentePlanoMedicoRepository.AddAsync(planoMedico);

        return await Task.FromResult(PacienteDependentePlanoMedicoResponse.MapFromEntity(planoMedico));
    }
}