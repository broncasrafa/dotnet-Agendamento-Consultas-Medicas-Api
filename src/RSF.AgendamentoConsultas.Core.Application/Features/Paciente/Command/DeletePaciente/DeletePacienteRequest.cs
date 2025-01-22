using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePaciente;

public record DeletePacienteRequest(int PacienteId) : IRequest<Result<bool>>;