using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.UpdateDependentePlanoMedico;

public record UpdatePacienteDependentePlanoMedicoRequest(
    int DependenteId, 
    int PacientePrincipalId, 
    int PlanoMedicoId, 
    int ConvenioMedicoId, 
    string NomePlano, 
    string NumeroCarteirinha) : IRequest<Result<bool>>;