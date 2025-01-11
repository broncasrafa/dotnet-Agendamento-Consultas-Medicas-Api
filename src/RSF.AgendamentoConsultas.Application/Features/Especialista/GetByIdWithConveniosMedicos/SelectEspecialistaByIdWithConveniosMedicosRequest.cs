using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByIdWithConveniosMedicos;

public record SelectEspecialistaByIdWithConveniosMedicosRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaConvenioMedicoResponse>>>;