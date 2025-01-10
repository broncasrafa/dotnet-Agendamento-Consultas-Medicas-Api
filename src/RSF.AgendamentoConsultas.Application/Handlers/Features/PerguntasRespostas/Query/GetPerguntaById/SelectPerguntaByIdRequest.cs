using RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Query.GetPerguntaById;

public record SelectPerguntaByIdRequest(int Id) : IRequest<Result<PerguntaResponse>>;