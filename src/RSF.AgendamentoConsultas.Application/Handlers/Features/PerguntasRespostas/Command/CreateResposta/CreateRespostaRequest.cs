using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Command.CreateResposta;

public record CreateRespostaRequest(int PerguntaId, string Resposta)
    : IRequest<Result<bool>>;