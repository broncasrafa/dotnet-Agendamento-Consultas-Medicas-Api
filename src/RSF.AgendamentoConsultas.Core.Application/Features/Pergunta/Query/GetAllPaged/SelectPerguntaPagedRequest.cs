using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetAllPaged;

public record SelectPerguntaPagedRequest(int PageSize = 15, int PageNum = 1) : IRequest<Result<PagedResult<PerguntaResponse>>>;