using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Command.CreatePergunta;

public record CreatePerguntaRequest(int EspecialistaId, int PacienteId, string Titulo, string Pergunta)
    : IRequest<Result<bool>>;