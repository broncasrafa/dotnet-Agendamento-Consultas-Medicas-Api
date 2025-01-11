using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.CreatePacientePlanoMedico;

public record CreatePacientePlanoMedicoRequest(int PacienteId, int ConvenioMedicoId, string NomePlano, string NumCartao) : IRequest<Result<PacientePlanoMedicoResponse>>;