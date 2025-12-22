using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.Like;

public record CreateLikeRespostaRequest(int RespostaId, int PacienteId) : IRequest<Result<bool>>;