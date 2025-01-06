using RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.GetByIdWithCidadesAtendidas;

public record SelectConvenioMedicoByIdWithCidadesAtendidasRequest(int Id) : IRequest<Result<ConvenioMedicoResponse>>;