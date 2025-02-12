using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Tags.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Tags.Query.GetAll;

public record SelectTagsRequest : IRequest<Result<IReadOnlyList<TagsResponse>>>;