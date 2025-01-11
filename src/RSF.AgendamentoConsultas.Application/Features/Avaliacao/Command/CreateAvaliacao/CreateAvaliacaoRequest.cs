using MediatR;

using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Avaliacao.Command.CreateAvaliacao;

public record CreateAvaliacaoRequest(int EspecialistaId, int PacienteId, string Feedback, int Score)
    : IRequest<Result<bool>>;