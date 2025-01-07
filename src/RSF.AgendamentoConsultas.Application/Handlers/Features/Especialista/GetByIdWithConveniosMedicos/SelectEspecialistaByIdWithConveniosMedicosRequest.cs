using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithConveniosMedicos;

public record SelectEspecialistaByIdWithConveniosMedicosRequest(int Id) : IRequest<Result<EspecialistaResultList<EspecialistaConvenioMedicoResponse>>>;