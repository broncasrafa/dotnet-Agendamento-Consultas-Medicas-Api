using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.CancelAgendamentoByEspecialista;

public record CancelAgendamentoByEspecialistaRequest(int AgendamentoId, int EspecialistaId) : IRequest<Result<bool>>;