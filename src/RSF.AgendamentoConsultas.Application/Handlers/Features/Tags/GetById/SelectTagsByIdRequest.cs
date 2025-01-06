using RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.GetById;

public record SelectTagsByIdRequest(int Id): IRequest<Result<TagsResponse>>;