using RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.GetAll;

public record SelectTagsRequest : IRequest<Result<IReadOnlyList<TagsResponse>>>;