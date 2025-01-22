using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaByIdRespostas;

public class SelectPerguntaByIdRespostasRequestHandler : IRequestHandler<SelectPerguntaByIdRespostasRequest, Result<PerguntaResultList<RespostaResponse>>>
{
    private readonly IPerguntaRepository _perguntaRepository;
         
    public SelectPerguntaByIdRespostasRequestHandler(IPerguntaRepository perguntaRepository) 
        => _perguntaRepository = perguntaRepository;

    public async Task<Result<PerguntaResultList<RespostaResponse>>> Handle(SelectPerguntaByIdRespostasRequest request, CancellationToken cancellationToken)
    {
        var pergunta = await _perguntaRepository.GetByIdAsync(request.PerguntaId);

        NotFoundException.ThrowIfNull(pergunta, $"Pergunta com o ID: '{request.PerguntaId}' não encontrada");

        var response = new PerguntaResultList<RespostaResponse>(request.PerguntaId, RespostaResponse.MapFromEntity(pergunta.Respostas));

        return Result.Success(response);
    }
}