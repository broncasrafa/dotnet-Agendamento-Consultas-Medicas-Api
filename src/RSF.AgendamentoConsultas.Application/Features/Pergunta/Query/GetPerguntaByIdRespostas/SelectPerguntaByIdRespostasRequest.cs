using RSF.AgendamentoConsultas.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.Query.GetPerguntaByIdRespostas;

public record SelectPerguntaByIdRespostasRequest(int PerguntaId) : IRequest<Result<PerguntaResultList<RespostaResponse>>>;