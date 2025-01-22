using RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Query.GetRespostasById;

public record SelectRespostaByIdRequest(int Id) : IRequest<Result<RespostaResponse>>;