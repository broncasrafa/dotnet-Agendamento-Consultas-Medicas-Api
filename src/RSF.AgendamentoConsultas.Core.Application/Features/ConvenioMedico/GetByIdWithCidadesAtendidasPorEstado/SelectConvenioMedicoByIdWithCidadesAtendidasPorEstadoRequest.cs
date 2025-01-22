using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.GetByIdWithCidadesAtendidasPorEstado;

public record SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequest(int Id, int EstadoId) : IRequest<Result<ConvenioMedicoResponse>>;