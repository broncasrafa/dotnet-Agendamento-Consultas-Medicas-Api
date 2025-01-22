using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithTags;

public record SelectEspecialistaByIdWithTagsRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaTagsResponse>>>;