using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithConveniosMedicos;

public record SelectEspecialistaByIdWithConveniosMedicosRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaConvenioMedicoResponse>>>;