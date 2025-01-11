using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.ConvenioMedico.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.ConvenioMedico.GetByIdWithCidadesAtendidas;

public record SelectConvenioMedicoByIdWithCidadesAtendidasRequest(int Id) : IRequest<Result<ConvenioMedicoResponse>>;