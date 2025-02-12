using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Estado.Query.GetById;

public record SelectEstadoByIdRequest(int Id) : IRequest<Result<EstadoResponse>>;