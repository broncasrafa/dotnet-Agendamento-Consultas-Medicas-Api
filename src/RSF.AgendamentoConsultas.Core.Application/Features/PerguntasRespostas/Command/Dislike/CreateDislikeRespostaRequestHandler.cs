using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.Dislike;

public class CreateDislikeRespostaRequestHandler : IRequestHandler<CreateDislikeRespostaRequest, Result<bool>>
{
    private readonly IPerguntaRespostaReacaoRepository _reacaoRepository;
    private readonly IPerguntaRespostaRepository _respostaRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public CreateDislikeRespostaRequestHandler(IPerguntaRespostaReacaoRepository reacaoRepository, IPerguntaRespostaRepository respostaRepository, IPacienteRepository pacienteRepository)
    {
        _reacaoRepository = reacaoRepository;
        _respostaRepository = respostaRepository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<bool>> Handle(CreateDislikeRespostaRequest request, CancellationToken cancellationToken)
    {
        var resposta = await _respostaRepository.GetByIdAsync(request.RespostaId);
        NotFoundException.ThrowIfNull(resposta, $"Resposta com o ID: '{request.RespostaId}' não encontrada");

        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não encontrado");
        
        var rowsAffected = await _reacaoRepository.DeleteReactionPerguntaRespostaAsync(request.RespostaId, request.PacienteId);

        return await Task.FromResult(rowsAffected > 0);
    }
}