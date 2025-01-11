using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Estado.GetById;

public record SelectEstadoByIdRequest(int Id) : IRequest<Result<EstadoResponse>>;