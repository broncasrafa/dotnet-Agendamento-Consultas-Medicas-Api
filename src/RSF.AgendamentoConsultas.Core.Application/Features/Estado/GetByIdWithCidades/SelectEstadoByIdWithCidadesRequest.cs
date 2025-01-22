using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Estado.GetByIdWithCidades;

public record SelectEstadoByIdWithCidadesRequest(int Id) : IRequest<Result<EstadoResponse>>;