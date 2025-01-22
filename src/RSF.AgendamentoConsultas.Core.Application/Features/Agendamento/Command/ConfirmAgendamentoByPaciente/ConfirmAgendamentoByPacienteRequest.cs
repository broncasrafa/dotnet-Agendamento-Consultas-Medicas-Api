using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByPaciente;

public record ConfirmAgendamentoByPacienteRequest(int AgendamentoId, int PacienteId) : IRequest<Result<bool>>;