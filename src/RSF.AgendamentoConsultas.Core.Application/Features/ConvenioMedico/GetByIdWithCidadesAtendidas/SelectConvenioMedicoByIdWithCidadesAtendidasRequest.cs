using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.GetByIdWithCidadesAtendidas;

public record SelectConvenioMedicoByIdWithCidadesAtendidasRequest(int Id) : IRequest<Result<ConvenioMedicoResponse>>;