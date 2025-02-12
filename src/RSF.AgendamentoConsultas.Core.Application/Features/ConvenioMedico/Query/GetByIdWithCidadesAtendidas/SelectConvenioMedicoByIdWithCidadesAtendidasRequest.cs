using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Query.GetByIdWithCidadesAtendidas;

public record SelectConvenioMedicoByIdWithCidadesAtendidasRequest(int Id) : IRequest<Result<ConvenioMedicoResponse>>;