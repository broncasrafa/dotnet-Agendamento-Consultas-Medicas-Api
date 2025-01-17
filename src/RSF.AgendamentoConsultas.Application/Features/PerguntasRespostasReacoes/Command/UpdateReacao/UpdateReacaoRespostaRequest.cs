using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostasReacoes.Command.UpdateReacao;

public record UpdateReacaoRespostaRequest(int ReacaoId, int RespostaId, int PacienteId, string Reacao)
    : IRequest<Result<bool>>;