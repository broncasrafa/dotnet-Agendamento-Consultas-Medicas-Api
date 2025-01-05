using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetById;

public record SelectEstadoByIdRequest(int Id) : IRequest<Result<EstadoResponse>>;