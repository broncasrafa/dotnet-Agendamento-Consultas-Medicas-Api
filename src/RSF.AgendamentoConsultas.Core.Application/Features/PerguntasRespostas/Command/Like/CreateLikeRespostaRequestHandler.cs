using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.Like;

public class CreateLikeRespostaRequestHandler : IRequestHandler<CreateLikeRespostaRequest, Result<bool>>
{
    private readonly IPerguntaRespostaReacaoRepository _reacaoRepository;
    private readonly IPerguntaRespostaRepository _respostaRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public CreateLikeRespostaRequestHandler(IPerguntaRespostaReacaoRepository reacaoRepository, IPerguntaRespostaRepository respostaRepository, IPacienteRepository pacienteRepository)
    {
        _reacaoRepository = reacaoRepository;
        _respostaRepository = respostaRepository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<bool>> Handle(CreateLikeRespostaRequest request, CancellationToken cancellationToken)
    {
        var resposta = await _respostaRepository.GetByIdAsync(request.RespostaId);
        NotFoundException.ThrowIfNull(resposta, $"Resposta com o ID: '{request.RespostaId}' não encontrada");

        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não encontrado");

        var reacao = new Domain.Entities.PerguntaRespostaReacao(request.RespostaId, request.PacienteId, reacao: ETipoReacaoResposta.Like);

        var rowsAffected = await _reacaoRepository.AddAsync(reacao);

        return await Task.FromResult(rowsAffected > 0);
    }
}