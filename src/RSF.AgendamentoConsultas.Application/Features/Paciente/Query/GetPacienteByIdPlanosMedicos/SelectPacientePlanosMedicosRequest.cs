using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Query.GetPacienteByIdPlanosMedicos;

public record SelectPacientePlanosMedicosRequest(int Id) : IRequest<Result<PacienteResultList<PacientePlanoMedicoResponse>>>;