using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Estado.GetAll;

public record SelectEstadoRequest() : IRequest<Result<IReadOnlyList<EstadoResponse>>>;