using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAgendamentos;

public class SelectPacienteAgendamentosRequestHandler : IRequestHandler<SelectPacienteAgendamentosRequest, Result<PacienteResultList<AgendamentoResponse>>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IAgendamentoConsultaRepository _agendamentoConsultaRepository;
    private readonly IHttpContextAccessor _httpContext;

    public SelectPacienteAgendamentosRequestHandler(
        IPacienteRepository pacienteRepository, 
        IAgendamentoConsultaRepository agendamentoConsultaRepository,
        IHttpContextAccessor httpContext)
    {
        _pacienteRepository = pacienteRepository;
        _agendamentoConsultaRepository = agendamentoConsultaRepository;
        _httpContext = httpContext;
    }

    public async Task<Result<PacienteResultList<AgendamentoResponse>>> Handle(SelectPacienteAgendamentosRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacienteId, ETipoPerfilAcesso.Paciente);

        var paciente = await _pacienteRepository.GetByIdDetailsAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var agendamentos = await _agendamentoConsultaRepository.GetAllByPacienteIdAsync(request.PacienteId);

        var response = new PacienteResultList<AgendamentoResponse>(request.PacienteId, AgendamentoResponse.MapFromEntity(agendamentos));

        return Result.Success(response);
    }
}