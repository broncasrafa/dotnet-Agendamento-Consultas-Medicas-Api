using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.ConfirmAgendamentoByEspecialista;

public record ConfirmAgendamentoByEspecialistaRequest(int AgendamentoId, int EspecialistaId) : IRequest<Result<bool>>;