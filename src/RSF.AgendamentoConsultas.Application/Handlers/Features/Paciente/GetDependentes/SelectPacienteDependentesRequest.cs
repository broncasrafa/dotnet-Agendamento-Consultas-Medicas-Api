using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.GetDependentes;

public record SelectPacienteDependentesRequest(int Id) : IRequest<Result<PacienteResultList<PacienteDependenteResponse>>>;