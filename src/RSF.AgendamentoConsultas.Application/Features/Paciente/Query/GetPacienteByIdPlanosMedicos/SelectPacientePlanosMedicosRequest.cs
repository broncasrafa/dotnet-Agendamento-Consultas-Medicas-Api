using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteByIdPlanosMedicos;

public record SelectPacientePlanosMedicosRequest(int PacienteId) : IRequest<Result<PacienteResultList<PacientePlanoMedicoResponse>>>;