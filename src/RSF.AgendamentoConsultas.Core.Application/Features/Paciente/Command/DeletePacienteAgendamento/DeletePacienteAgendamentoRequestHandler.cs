using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePacienteAgendamento;

public class DeletePacienteAgendamentoRequestHandler : IRequestHandler<DeletePacienteAgendamentoRequest, Result<bool>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IAgendamentoConsultaRepository _agendamentoConsultaRepository;
    private readonly IHttpContextAccessor _httpContext;

    public DeletePacienteAgendamentoRequestHandler(
        IPacienteRepository pacienteRepository, 
        IAgendamentoConsultaRepository agendamentoConsultaRepository, 
        IHttpContextAccessor httpContext)
    {
        _pacienteRepository = pacienteRepository;
        _agendamentoConsultaRepository = agendamentoConsultaRepository;
        _httpContext = httpContext;
    }

    public async Task<Result<bool>> Handle(DeletePacienteAgendamentoRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacienteId, ETipoPerfilAcesso.Paciente);

        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var agendamentoPaciente = await _agendamentoConsultaRepository.GetByIdAsync(request.AgendamentoId, request.PacienteId);
        NotFoundException.ThrowIfNull(agendamentoPaciente, $"Agendamento com o ID: '{request.AgendamentoId}' não foi encontrado para o Paciente com o ID: '{request.PacienteId}'");

        agendamentoPaciente.Cancelar(request.NotaCancelamento);

        var rowsAffected = await _agendamentoConsultaRepository.UpdateAsync(agendamentoPaciente);

        return await Task.FromResult(rowsAffected > 0);
    }
}