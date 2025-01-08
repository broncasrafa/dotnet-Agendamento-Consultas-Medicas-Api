using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.GetPlanosMedicos;

public record SelectPacientePlanosMedicosRequest(int Id) : IRequest<Result<PacienteResultList<PacientePlanoMedicoResponse>>>;