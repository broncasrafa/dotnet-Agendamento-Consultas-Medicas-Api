using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostasReacoes.Command.CreateReacao;

public record CreateReacaoRespostaRequest(int RespostaId, int PacienteId, string Reacao) : IRequest<Result<bool>>;