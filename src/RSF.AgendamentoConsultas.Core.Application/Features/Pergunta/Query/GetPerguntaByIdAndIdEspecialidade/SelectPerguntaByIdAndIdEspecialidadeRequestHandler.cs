using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaByIdAndIdEspecialidade;

public class SelectPerguntaByIdAndIdEspecialidadeRequestHandler : IRequestHandler<SelectPerguntaByIdAndIdEspecialidadeRequest, Result<PerguntaResponse>>
{
    private readonly IPerguntaRepository _perguntaRepository;
         
    public SelectPerguntaByIdAndIdEspecialidadeRequestHandler(IPerguntaRepository perguntaRepository) 
        => _perguntaRepository = perguntaRepository;

    public async Task<Result<PerguntaResponse>> Handle(SelectPerguntaByIdAndIdEspecialidadeRequest request, CancellationToken cancellationToken)
    {
        var pergunta = await _perguntaRepository.GetByIdAsync(request.PerguntaId, request.EspecialidadeId);

        NotFoundException.ThrowIfNull(pergunta, $"Pergunta com o ID: '{request.PerguntaId}' na Especialidade ID: '{request.EspecialidadeId}' não encontrada");

        return await Task.FromResult(PerguntaResponse.MapFromEntity(pergunta));
    }
}