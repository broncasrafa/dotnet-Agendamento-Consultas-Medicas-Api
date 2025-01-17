using RSF.AgendamentoConsultas.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.Query.GetPerguntaById;

public class SelectPerguntaByIdRequestHandler : IRequestHandler<SelectPerguntaByIdRequest, Result<PerguntaResponse>>
{
    private readonly IPerguntaRepository _perguntaRepository;
         
    public SelectPerguntaByIdRequestHandler(IPerguntaRepository perguntaRepository) 
        => _perguntaRepository = perguntaRepository;

    public async Task<Result<PerguntaResponse>> Handle(SelectPerguntaByIdRequest request, CancellationToken cancellationToken)
    {
        var pergunta = await _perguntaRepository.GetByIdAsync(request.PerguntaId);

        NotFoundException.ThrowIfNull(pergunta, $"Pergunta com o ID: '{request.PerguntaId}' não encontrada");

        return await Task.FromResult(PerguntaResponse.MapFromEntity(pergunta));
    }
}