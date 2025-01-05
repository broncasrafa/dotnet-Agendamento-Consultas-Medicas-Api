using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;

public record SelectEstadoRequest() : IRequest<Result<IReadOnlyList<EstadoResponse>>>;