using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetEspecialistaPerguntas;

public class SelectEspecialistaPerguntasRequestHandler : IRequestHandler<SelectEspecialistaPerguntasRequest, Result<PagedResult<PerguntaResponse>>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IPerguntaRepository _perguntaRepository;

    public SelectEspecialistaPerguntasRequestHandler(IEspecialistaRepository especialistaRepository, IPerguntaRepository perguntaRepository)
    {
        _especialistaRepository = especialistaRepository;
        _perguntaRepository = perguntaRepository;
    }

    public async Task<Result<PagedResult<PerguntaResponse>>> Handle(SelectEspecialistaPerguntasRequest request, CancellationToken cancellationToken)
    {
        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var perguntaIds = especialista.PerguntasEspecialista.Select(p => p.PerguntaId).ToList();
        NotFoundException.ThrowIfNull(perguntaIds, $"Especialista com o ID: '{request.EspecialistaId}' não possui perguntas até o momento");

        var pagedResult = await _perguntaRepository.GetByListaPerguntaIdsPagedAsync(perguntaIds, request.PageNum, request.PageSize);
        var response = PerguntaResponse.MapFromEntityPaged(pagedResult, request.PageNum, request.PageSize);

        return Result.Success(response);
    }
}