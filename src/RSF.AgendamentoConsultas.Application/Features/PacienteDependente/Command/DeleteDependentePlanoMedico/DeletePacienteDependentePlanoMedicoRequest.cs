using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.DeleteDependentePlanoMedico;

public record DeletePacienteDependentePlanoMedicoRequest(
    int DependenteId, 
    int PacientePrincipalId,
    int PlanoMedicoId) : IRequest<Result<bool>>;