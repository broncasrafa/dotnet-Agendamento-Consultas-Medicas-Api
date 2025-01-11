using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Query.GetDependenteById;

public record SelectPacienteDependenteByIdRequest(int DependenteId) : IRequest<Result<PacienteDependenteResponse>>;