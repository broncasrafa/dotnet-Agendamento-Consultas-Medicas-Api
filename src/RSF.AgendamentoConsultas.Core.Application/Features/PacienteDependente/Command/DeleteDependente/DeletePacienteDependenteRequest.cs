using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.DeleteDependente;

public record DeletePacienteDependenteRequest(int DependenteId, int PacientePrincipalId) : IRequest<Result<bool>>;