using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;

public record SelectPacienteDependentePlanosMedicosRequest(int DependenteId)
    : IRequest<Result<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>>;