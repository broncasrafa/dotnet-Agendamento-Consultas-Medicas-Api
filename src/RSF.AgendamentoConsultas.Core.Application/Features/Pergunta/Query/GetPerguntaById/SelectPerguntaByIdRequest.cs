using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaById;

public record SelectPerguntaByIdRequest(int PerguntaId) : IRequest<Result<PerguntaResponse>>;