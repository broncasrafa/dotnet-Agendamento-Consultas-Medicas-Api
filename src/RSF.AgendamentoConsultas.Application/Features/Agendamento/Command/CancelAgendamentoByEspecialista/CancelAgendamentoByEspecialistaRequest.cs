using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.CancelAgendamentoByEspecialista;

public record CancelAgendamentoByEspecialistaRequest(int AgendamentoId, int EspecialistaId) : IRequest<Result<bool>>;