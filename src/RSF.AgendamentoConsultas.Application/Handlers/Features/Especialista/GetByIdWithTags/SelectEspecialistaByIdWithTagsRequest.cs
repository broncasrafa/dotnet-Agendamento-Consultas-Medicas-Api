using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithTags;

public record SelectEspecialistaByIdWithTagsRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaTagsResponse>>>;