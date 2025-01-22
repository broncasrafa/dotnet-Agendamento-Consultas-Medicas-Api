using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaByIdRespostas;

public record SelectPerguntaByIdRespostasRequest(int PerguntaId) : IRequest<Result<PerguntaResultList<RespostaResponse>>>;