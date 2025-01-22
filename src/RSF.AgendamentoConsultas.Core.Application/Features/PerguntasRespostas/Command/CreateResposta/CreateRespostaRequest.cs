using OperationResult;
using MediatR;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.CreateResposta;

public record CreateRespostaRequest(
    int PerguntaId, 
    int EspecialistaId, 
    string Resposta): IRequest<Result<bool>>;