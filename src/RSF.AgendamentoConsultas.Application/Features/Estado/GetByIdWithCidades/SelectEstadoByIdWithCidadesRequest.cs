using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Estado.GetByIdWithCidades;

public record SelectEstadoByIdWithCidadesRequest(int Id) : IRequest<Result<EstadoResponse>>;