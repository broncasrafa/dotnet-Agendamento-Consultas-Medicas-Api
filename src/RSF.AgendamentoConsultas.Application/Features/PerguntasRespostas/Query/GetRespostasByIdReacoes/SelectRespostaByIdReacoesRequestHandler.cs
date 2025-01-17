using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetRespostasByIdReacoes;

public class SelectRespostaByIdReacoesRequestHandler : IRequestHandler<SelectRespostaByIdReacoesRequest, Result<RespostaResponse>>
{
    private readonly IPerguntaRespostaRepository _perguntaRespostaRepository;

    public SelectRespostaByIdReacoesRequestHandler(IPerguntaRespostaRepository perguntaRespostaRepository)
        => _perguntaRespostaRepository = perguntaRespostaRepository;

    public async Task<Result<RespostaResponse>> Handle(SelectRespostaByIdReacoesRequest request, CancellationToken cancellationToken)
    {
        var result = await _perguntaRespostaRepository.GetByIdWithReacoesAsync(request.Id);

        NotFoundException.ThrowIfNull(result, $"Resposta com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(RespostaResponse.MapFromEntity(result));
    }
}