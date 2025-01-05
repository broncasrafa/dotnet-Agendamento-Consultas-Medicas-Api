using RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.Response;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.GetByIdWithCidadesAtendidasPorEstado;

public record SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequest(int Id, int EstadoId) : IRequest<Result<ConvenioMedicoResponse>>;