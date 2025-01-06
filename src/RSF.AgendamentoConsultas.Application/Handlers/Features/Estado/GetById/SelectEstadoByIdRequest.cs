using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetById;

public record SelectEstadoByIdRequest(int Id) : IRequest<Result<EstadoResponse>>;