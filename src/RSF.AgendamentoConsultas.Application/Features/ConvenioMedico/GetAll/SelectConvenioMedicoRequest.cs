using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.ConvenioMedico.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.ConvenioMedico.GetAll;

public record SelectConvenioMedicoRequest() : IRequest<Result<IReadOnlyList<ConvenioMedicoResponse>>>;