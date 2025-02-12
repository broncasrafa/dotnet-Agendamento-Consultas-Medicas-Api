using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Query.GetById;

public record SelectConvenioMedicoByIdRequest(int Id) : IRequest<Result<ConvenioMedicoResponse>>;