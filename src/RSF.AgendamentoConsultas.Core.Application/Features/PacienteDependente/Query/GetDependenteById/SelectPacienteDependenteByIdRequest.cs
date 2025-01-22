using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteById;

public record SelectPacienteDependenteByIdRequest(int DependenteId) : IRequest<Result<PacienteDependenteResponse>>;