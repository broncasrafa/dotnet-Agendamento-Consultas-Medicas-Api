using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Command.CreatePergunta;

public record CreatePerguntaEspecialidadeRequest(
    int EspecialidadeId, 
    int PacienteId, 
    string Pergunta, 
    bool TermosUsoPolitica) : IRequest<Result<bool>>;