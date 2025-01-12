using RSF.AgendamentoConsultas.Application.Features.Agendamento.Responses;
using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteByIdAgendamentos;

public record SelectPacienteAgendamentosRequest(int PacienteId) : IRequest<Result<PacienteResultList<AgendamentoResponse>>>;