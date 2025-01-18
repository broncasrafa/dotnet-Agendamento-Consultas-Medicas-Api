using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.CancelAgendamentoByPaciente;

public record CancelAgendamentoByPacienteRequest(
    int AgendamentoId, 
    int PacienteId,
    int? DependenteId,
    string MotivoCancelamento) : IRequest<Result<bool>>;