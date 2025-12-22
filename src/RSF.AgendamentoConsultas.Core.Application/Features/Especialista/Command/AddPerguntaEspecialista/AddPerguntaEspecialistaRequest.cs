using OperationResult;
using MediatR;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddPerguntaEspecialista;

public record AddPerguntaEspecialistaRequest(
    int EspecialistaId,
    int PacienteId,
    string Pergunta,
    bool TermosUsoPolitica
    ) : IRequest<Result<bool>>;