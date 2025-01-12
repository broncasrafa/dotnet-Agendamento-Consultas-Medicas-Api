using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteByIdDependentes;

public record SelectPacienteDependentesRequest(int PacienteId) : IRequest<Result<PacienteResultList<PacienteDependenteResponse>>>;