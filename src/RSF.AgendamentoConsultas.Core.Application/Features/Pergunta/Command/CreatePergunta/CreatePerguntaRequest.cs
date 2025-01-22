using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Command.CreatePergunta;

public record CreatePerguntaRequest(
    int EspecialidadeId, 
    int PacienteId, 
    string Pergunta, 
    bool TermosUsoPolitica) : IRequest<Result<bool>>;