using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostasReacoes.Command.UpdateReacao;

public record UpdateReacaoRespostaRequest(int ReacaoId, int RespostaId, int PacienteId, string Reacao)
    : IRequest<Result<bool>>;