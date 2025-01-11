using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetRespostasByPerguntaId;

public record SelectRespostaByIdRequest(int Id) : IRequest<Result<RespostaResponse>>;