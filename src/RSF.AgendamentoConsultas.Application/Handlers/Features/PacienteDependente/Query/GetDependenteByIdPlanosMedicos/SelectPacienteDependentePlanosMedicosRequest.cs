using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;

public record SelectPacienteDependentePlanosMedicosRequest(int DependenteId, int PacientePrincipalId)
    : IRequest<Result<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>>;