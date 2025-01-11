using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.CreateDependentePlanoMedico;

public record CreatePacienteDependentePlanoMedicoRequest(int DependenteId, int PacientePrincipalId, int ConvenioMedicoId, string NomePlano, string NumCartao)
    : IRequest<Result<PacienteDependentePlanoMedicoResponse>>;