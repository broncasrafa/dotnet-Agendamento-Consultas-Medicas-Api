using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetEspecialistaPerguntas;

public record SelectEspecialistaPerguntasRequest(int EspecialistaId, int PageSize = 10, int PageNum = 1) 
    : IRequest<Result<PagedResult<PerguntaResponse>>>
{
}