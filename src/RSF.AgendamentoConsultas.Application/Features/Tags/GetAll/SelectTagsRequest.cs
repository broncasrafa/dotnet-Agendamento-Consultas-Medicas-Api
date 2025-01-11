using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Tags.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Tags.GetAll;

public record SelectTagsRequest : IRequest<Result<IReadOnlyList<TagsResponse>>>;