using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.CreateDependentePlanoMedico;

public record CreatePacienteDependentePlanoMedicoRequest(
    int DependenteId, 
    int PacientePrincipalId, 
    int ConvenioMedicoId, 
    string NomePlano, 
    string NumCartao)
    : IRequest<Result<PacienteDependentePlanoMedicoResponse>>;