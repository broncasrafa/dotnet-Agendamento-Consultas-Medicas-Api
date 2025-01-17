using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetRespostasByIdReacoes;

public record SelectRespostaByIdReacoesRequest(int Id) : IRequest<Result<RespostaResponse>>;