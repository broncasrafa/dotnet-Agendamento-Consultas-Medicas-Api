using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.DeleteDependente;

public record DeletePacienteDependenteRequest(int DependenteId, int PacientePrincipalId) : IRequest<Result<bool>>;