using RSF.AgendamentoConsultas.Application.Features.Pergunta.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.Query.GetPerguntaByIdAndIdEspecialidade;

public record SelectPerguntaByIdAndIdEspecialidadeRequest(
    int PerguntaId,
    int EspecialidadeId) : IRequest<Result<PerguntaResponse>>;