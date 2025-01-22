using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAgendamentos;

public record SelectPacienteAgendamentosRequest(int PacienteId) : IRequest<Result<PacienteResultList<AgendamentoResponse>>>;