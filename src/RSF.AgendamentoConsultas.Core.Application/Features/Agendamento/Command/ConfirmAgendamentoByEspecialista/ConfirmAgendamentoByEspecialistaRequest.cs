using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Command.ConfirmAgendamentoByEspecialista;

public record ConfirmAgendamentoByEspecialistaRequest(int AgendamentoId, int EspecialistaId) : IRequest<Result<bool>>;