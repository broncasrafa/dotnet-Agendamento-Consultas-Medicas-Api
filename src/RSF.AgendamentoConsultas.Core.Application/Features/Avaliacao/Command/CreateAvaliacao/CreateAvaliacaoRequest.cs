using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Command.CreateAvaliacao;

public record CreateAvaliacaoRequest(int EspecialistaId, int PacienteId, int? TagId, string Feedback, int Score)
    : IRequest<Result<bool>>;