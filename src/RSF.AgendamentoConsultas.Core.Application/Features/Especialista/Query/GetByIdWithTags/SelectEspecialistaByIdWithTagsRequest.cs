using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithTags;

public record SelectEspecialistaByIdWithTagsRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaTagsResponse>>>;