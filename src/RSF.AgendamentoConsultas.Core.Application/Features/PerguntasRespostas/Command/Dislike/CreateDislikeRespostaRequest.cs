using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.Dislike;

public record CreateDislikeRespostaRequest(int RespostaId, int PacienteId) : IRequest<Result<bool>>;