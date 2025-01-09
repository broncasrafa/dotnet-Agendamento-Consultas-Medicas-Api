using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Query.GetPacienteByIdPlanosMedicos;

public record SelectPacientePlanosMedicosRequest(int Id) : IRequest<Result<PacienteResultList<PacientePlanoMedicoResponse>>>;