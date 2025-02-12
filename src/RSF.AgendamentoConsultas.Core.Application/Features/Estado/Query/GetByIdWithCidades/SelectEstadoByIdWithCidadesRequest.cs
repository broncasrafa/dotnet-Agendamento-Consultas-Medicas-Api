using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Estado.Query.GetByIdWithCidades;

public record SelectEstadoByIdWithCidadesRequest(int Id) : IRequest<Result<EstadoResponse>>;