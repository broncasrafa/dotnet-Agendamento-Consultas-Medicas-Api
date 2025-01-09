using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Query.GetDependenteById;

public record SelectPacienteDependenteByIdRequest(int PacientePrincipalId, int DependenteId) : IRequest<Result<PacienteDependenteResponse>>;