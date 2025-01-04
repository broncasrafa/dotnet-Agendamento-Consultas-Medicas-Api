using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetByIdWithCidades;

public record SelectEstadoByIdWithCidadesRequest(int Id) : IRequest<Result<SelectEstadoResponse>>;