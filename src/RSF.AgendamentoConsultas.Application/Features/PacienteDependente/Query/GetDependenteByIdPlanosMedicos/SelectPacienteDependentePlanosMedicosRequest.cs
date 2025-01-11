using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;

public record SelectPacienteDependentePlanosMedicosRequest(int PacientePrincipalId, int DependenteId)
    : IRequest<Result<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>>;