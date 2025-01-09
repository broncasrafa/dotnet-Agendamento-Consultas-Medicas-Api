using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Query.GetPacienteByIdDependentes;

public record SelectPacienteDependentesRequest(int Id) : IRequest<Result<PacienteResultList<PacienteDependenteResponse>>>;