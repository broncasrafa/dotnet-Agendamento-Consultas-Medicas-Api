using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostasReacoes.Command.CreateReacao;

public class CreateReacaoRespostaRequestHandler : IRequestHandler<CreateReacaoRespostaRequest, Result<bool>>
{
    private readonly IPerguntaRespostaReacaoRepository _reacaoRepository;
    private readonly IPerguntaRespostaRepository _respostaRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public CreateReacaoRespostaRequestHandler(
        IPerguntaRespostaReacaoRepository perguntaRespostaReacaoRepository, 
        IPerguntaRespostaRepository respostaRepository, 
        IPacienteRepository pacienteRepository)
    {
        _reacaoRepository = perguntaRespostaReacaoRepository;
        _respostaRepository = respostaRepository;
        _pacienteRepository = pacienteRepository;
    }


    public async Task<Result<bool>> Handle(CreateReacaoRespostaRequest request, CancellationToken cancellationToken)
    {
        var resposta = await _respostaRepository.GetByIdAsync(request.RespostaId);
        NotFoundException.ThrowIfNull(resposta, $"Resposta com o ID: '{request.RespostaId}' não encontrada");

        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não encontrado");

        var tipoReacao = Domain.Entities.PerguntaRespostaReacao.ConverterReacaoParaEnum(request.Reacao);
        if (tipoReacao == ETipoReacaoResposta.None)
            throw new ArgumentException("Tipo de reação inválida. Valores possiveis válidos são 'Like' e 'Dislike' ");

        var reacao = new Domain.Entities.PerguntaRespostaReacao(request.RespostaId, request.PacienteId, tipoReacao);

        var rowsAffected = await _reacaoRepository.AddAsync(reacao);

        return await Task.FromResult(rowsAffected > 0);
    }
}