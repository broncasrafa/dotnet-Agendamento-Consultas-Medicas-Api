using MediatR;

using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.DeleteDependentePlanoMedico;

public record DeletePacienteDependentePlanoMedicoRequest(int PlanoMedicoId, int DependenteId, int PacientePrincipalId)
    : IRequest<Result<bool>>;