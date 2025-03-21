using OperationResult;
using MediatR;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddPerguntaEspecialista;

public record CreatePerguntaEspecialistaRequest(
    int EspecialistaId,
    int PacienteId,
    string Pergunta,
    bool TermosUsoPolitica
    ) : IRequest<Result<bool>>;