using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetById;

public record SelectEspecialistaByIdRequest(int Id) : IRequest<Result<EspecialistaResponse>>;