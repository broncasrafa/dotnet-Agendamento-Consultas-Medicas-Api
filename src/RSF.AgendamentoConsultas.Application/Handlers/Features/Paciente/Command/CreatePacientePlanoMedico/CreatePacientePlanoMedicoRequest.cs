using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Command.CreatePacientePlanoMedico;

public record CreatePacientePlanoMedicoRequest(int PacienteId, int ConvenioMedicoId, string NomePlano, string NumCartao) : IRequest<Result<PacientePlanoMedicoResponse>>;