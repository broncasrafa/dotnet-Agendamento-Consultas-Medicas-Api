using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Query.GetDependenteById;

public record SelectPacienteDependenteByIdRequest(int PacientePrincipalId, int DependenteId) : IRequest<Result<PacienteDependenteResponse>>;