using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetRespostasByPerguntaId;

public class SelectRespostaByIdRequestHandler : IRequestHandler<SelectRespostaByIdRequest, Result<RespostaResponse>>
{
    private readonly IBaseRepository<PerguntaResposta> _especialistaRespostaPerguntaRepository;

    public SelectRespostaByIdRequestHandler(IBaseRepository<PerguntaResposta> especialistaRespostaPerguntaRepository)
        => _especialistaRespostaPerguntaRepository = especialistaRespostaPerguntaRepository;

    public async Task<Result<RespostaResponse>> Handle(SelectRespostaByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _especialistaRespostaPerguntaRepository.GetByFilterAsync(c => c.PerguntaRespostaId == request.Id, c => c.Pergunta);

        NotFoundException.ThrowIfNull(result, $"Resposta com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(RespostaResponse.MapFromEntity(result));
    }
}