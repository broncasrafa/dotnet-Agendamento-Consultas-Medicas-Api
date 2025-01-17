using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetRespostasById;

public record SelectRespostaByIdRequest(int Id) : IRequest<Result<RespostaResponse>>;