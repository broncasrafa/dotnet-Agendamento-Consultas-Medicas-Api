using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Command.CreateResposta;

public class CreateRespostaRequestHandler : IRequestHandler<CreateRespostaRequest, Result<bool>>
{    
    private readonly IBaseRepository<EspecialistaRespostaPergunta> _especialistaRespostaPerguntaRepository;
    private readonly IBaseRepository<EspecialistaPergunta> _especialistaPerguntaRepository;

    public CreateRespostaRequestHandler(
        IBaseRepository<EspecialistaRespostaPergunta> especialistaRespostaPerguntaRepository, 
        IBaseRepository<EspecialistaPergunta> especialistaPerguntaRepository)
    {
        _especialistaRespostaPerguntaRepository = especialistaRespostaPerguntaRepository;
        _especialistaPerguntaRepository = especialistaPerguntaRepository;
    }

    public async Task<Result<bool>> Handle(CreateRespostaRequest request, CancellationToken cancellationToken)
    {
        var pergunta = await _especialistaPerguntaRepository.GetByIdAsync(request.PerguntaId);
        NotFoundException.ThrowIfNull(pergunta, $"Pergunta com o ID: '{request.PerguntaId}' não encontrada");

        var resposta = new EspecialistaRespostaPergunta(request.PerguntaId, request.Resposta);
        await _especialistaRespostaPerguntaRepository.AddAsync(resposta);
        var rowsAffected = await _especialistaRespostaPerguntaRepository.SaveChangesAsync();

        return await Task.FromResult(rowsAffected > 0);
    }
}