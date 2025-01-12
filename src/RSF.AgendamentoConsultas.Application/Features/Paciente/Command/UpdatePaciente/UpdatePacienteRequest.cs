using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Command.UpdatePaciente;

public record UpdatePacienteRequest(
    int PacienteId,
    string NomeCompleto,
    string Email,
    string Telefone,
    string Genero,
    DateTime DataNascimento
) : IRequest<Result<bool>>;