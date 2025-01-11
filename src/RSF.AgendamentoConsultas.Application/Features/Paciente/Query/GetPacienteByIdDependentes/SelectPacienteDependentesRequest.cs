using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteByIdDependentes;

public record SelectPacienteDependentesRequest(int Id) : IRequest<Result<PacienteResultList<PacienteDependenteResponse>>>;