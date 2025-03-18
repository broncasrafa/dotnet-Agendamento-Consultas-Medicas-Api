using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetAllPaged;

public class SelectPerguntaPagedRequestHandler : IRequestHandler<SelectPerguntaPagedRequest, Result<PagedResult<PerguntaResponse>>>
{
    private readonly IPerguntaRepository _perguntaRepository;

    public SelectPerguntaPagedRequestHandler(IPerguntaRepository perguntaRepository) => _perguntaRepository = perguntaRepository;

    public async Task<Result<PagedResult<PerguntaResponse>>> Handle(SelectPerguntaPagedRequest request, CancellationToken cancellationToken)
    {
        var pagedResult = await _perguntaRepository.GetAllPagedAsync(request.PageNum, request.PageSize);

        var response = PerguntaResponse.MapFromEntityPaged(pagedResult, request.PageNum, request.PageSize);

        return Result.Success(response);
    }
}