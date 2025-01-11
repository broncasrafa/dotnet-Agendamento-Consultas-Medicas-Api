using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Avaliacao.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Avaliacao.Query.GetAvaliacaoById;

public record SelectAvaliacaoByIdRequest(int Id) : IRequest<Result<AvaliacaoResponse>>;