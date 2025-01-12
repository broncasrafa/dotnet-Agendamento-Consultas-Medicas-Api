using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.DeletePaciente;

public record DeletePacienteRequest(int PacienteId) : IRequest<Result<bool>>;