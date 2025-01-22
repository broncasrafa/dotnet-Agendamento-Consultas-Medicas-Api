using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Tags.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Tags.GetAll;

public record SelectTagsRequest : IRequest<Result<IReadOnlyList<TagsResponse>>>;