using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Estado.GetAll;

public record SelectEstadoRequest() : IRequest<Result<IReadOnlyList<EstadoResponse>>>;