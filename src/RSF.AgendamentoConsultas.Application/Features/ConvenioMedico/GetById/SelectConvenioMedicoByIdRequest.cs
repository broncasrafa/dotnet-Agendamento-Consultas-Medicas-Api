using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.ConvenioMedico.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.ConvenioMedico.GetById;

public record SelectConvenioMedicoByIdRequest(int Id) : IRequest<Result<ConvenioMedicoResponse>>;