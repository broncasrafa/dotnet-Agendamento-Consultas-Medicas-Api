using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;

public record SelectPacienteDependentePlanosMedicosRequest(int DependenteId)
    : IRequest<Result<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>>;