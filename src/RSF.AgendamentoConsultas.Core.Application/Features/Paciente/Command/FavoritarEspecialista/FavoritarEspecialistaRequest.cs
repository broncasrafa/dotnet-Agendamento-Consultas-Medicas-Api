using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.FavoritarEspecialista;

public record FavoritarEspecialistaRequest(int PacienteId, int EspecialistaId) : IRequest<Result<bool>>;