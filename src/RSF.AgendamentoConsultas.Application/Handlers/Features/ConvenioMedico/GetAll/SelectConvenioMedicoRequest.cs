using RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.Response;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.GetAll;

public record SelectConvenioMedicoRequest() : IRequest<Result<IReadOnlyList<ConvenioMedicoResponse>>>;