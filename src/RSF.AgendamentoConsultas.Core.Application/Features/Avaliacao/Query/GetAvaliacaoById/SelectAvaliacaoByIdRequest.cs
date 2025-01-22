using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Query.GetAvaliacaoById;

public record SelectAvaliacaoByIdRequest(int Id) : IRequest<Result<AvaliacaoResponse>>;