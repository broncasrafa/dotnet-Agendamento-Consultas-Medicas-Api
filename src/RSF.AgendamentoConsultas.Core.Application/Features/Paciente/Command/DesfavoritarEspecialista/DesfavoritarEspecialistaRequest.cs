using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DesfavoritarEspecialista;

public record DesfavoritarEspecialistaRequest(int PacienteId, int EspecialistaId) : IRequest<Result<bool>>;