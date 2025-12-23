using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByUserId;

public record SelectEspecialistaByUserIdRequest(Guid UserId) : IRequest<Result<EspecialistaResponse>>;