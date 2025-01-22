using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAgendamentos;

public class SelectPacienteAgendamentosRequestHandler : IRequestHandler<SelectPacienteAgendamentosRequest, Result<PacienteResultList<AgendamentoResponse>>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IAgendamentoConsultaRepository _agendamentoConsultaRepository;

    public SelectPacienteAgendamentosRequestHandler(IPacienteRepository pacienteRepository, IAgendamentoConsultaRepository agendamentoConsultaRepository)
    {
        _pacienteRepository = pacienteRepository;
        _agendamentoConsultaRepository = agendamentoConsultaRepository;
    }

    public async Task<Result<PacienteResultList<AgendamentoResponse>>> Handle(SelectPacienteAgendamentosRequest request, CancellationToken cancellationToken)
    {
        var paciente = await _pacienteRepository.GetByIdDetailsAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var agendamentos = await _agendamentoConsultaRepository.GetAllByPacienteIdAsync(request.PacienteId);

        var response = new PacienteResultList<AgendamentoResponse>(request.PacienteId, AgendamentoResponse.MapFromEntity(agendamentos));

        return Result.Success(response);
    }
}