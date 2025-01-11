using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetPerguntaById;

public record SelectPerguntaByIdRequest(int Id) : IRequest<Result<PerguntaResponse>>;