using RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.Response;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.GetById;

public record SelectConvenioMedicoByIdRequest(int Id) : IRequest<Result<ConvenioMedicoResponse>>;