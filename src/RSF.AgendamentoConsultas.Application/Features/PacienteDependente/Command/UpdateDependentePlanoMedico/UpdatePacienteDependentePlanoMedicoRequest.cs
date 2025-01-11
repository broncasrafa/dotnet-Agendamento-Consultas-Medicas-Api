using MediatR;

using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.UpdateDependentePlanoMedico;

public record UpdatePacienteDependentePlanoMedicoRequest(int PlanoMedicoId, int DependenteId, int PacientePrincipalId, int ConvenioMedicoId, string NomePlano, string NumeroCarteirinha)
    : IRequest<Result<bool>>;