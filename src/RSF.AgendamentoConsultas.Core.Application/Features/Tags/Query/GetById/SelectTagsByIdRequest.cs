using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Tags.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Tags.Query.GetById;

public record SelectTagsByIdRequest(int Id) : IRequest<Result<TagsResponse>>;