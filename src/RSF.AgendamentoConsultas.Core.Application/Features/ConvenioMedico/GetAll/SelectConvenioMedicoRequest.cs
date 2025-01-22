using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.GetAll;

public record SelectConvenioMedicoRequest() : IRequest<Result<IReadOnlyList<ConvenioMedicoResponse>>>;