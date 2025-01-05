using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetByIdWithCidades;

public record SelectEstadoByIdWithCidadesRequest(int Id) : IRequest<Result<EstadoResponse>>;