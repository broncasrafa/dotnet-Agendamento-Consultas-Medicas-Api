using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostasReacoes.Command.CreateReacao;

public record CreateReacaoRespostaRequest(int RespostaId, int PacienteId, string Reacao) : IRequest<Result<bool>>;