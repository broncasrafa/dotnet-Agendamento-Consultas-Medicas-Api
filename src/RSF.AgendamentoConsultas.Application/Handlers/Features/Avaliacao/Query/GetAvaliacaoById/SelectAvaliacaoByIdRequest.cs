using RSF.AgendamentoConsultas.Application.Handlers.Features.Avaliacao.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Avaliacao.Query.GetAvaliacaoById;

public record SelectAvaliacaoByIdRequest(int Id) : IRequest<Result<AvaliacaoResponse>>;