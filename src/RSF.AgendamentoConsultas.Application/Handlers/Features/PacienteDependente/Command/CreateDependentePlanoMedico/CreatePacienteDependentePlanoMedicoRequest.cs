using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Command.CreateDependentePlanoMedico;

public record CreatePacienteDependentePlanoMedicoRequest(int DependenteId, int ConvenioMedicoId, string NomePlano, string NumCartao)
    : IRequest<Result<PacienteDependentePlanoMedicoResponse>>;