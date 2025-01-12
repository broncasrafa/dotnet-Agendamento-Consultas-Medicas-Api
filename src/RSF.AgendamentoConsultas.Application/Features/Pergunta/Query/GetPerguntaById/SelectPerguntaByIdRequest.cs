using RSF.AgendamentoConsultas.Application.Features.Pergunta.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.Query.GetPerguntaById;

public record SelectPerguntaByIdRequest(int Id) : IRequest<Result<PerguntaResponse>>;