using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostasReacoes.Command.UpdateReacao;

public class UpdateReacaoRespostaRequestHandler : IRequestHandler<UpdateReacaoRespostaRequest, Result<bool>>
{
    private readonly IPerguntaRespostaReacaoRepository _reacaoRepository;
    private readonly IPerguntaRespostaRepository _respostaRepository;
    private readonly IPacienteRepository _pacienteRepository;

    public UpdateReacaoRespostaRequestHandler(
        IPerguntaRespostaReacaoRepository reacaoRepository, 
        IPerguntaRespostaRepository respostaRepository, 
        IPacienteRepository pacienteRepository)
    {
        _reacaoRepository = reacaoRepository;
        _respostaRepository = respostaRepository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<bool>> Handle(UpdateReacaoRespostaRequest request, CancellationToken cancellationToken)
    {
        var resposta = await _respostaRepository.GetByIdAsync(request.RespostaId);
        NotFoundException.ThrowIfNull(resposta, $"Resposta com o ID: '{request.RespostaId}' não encontrada");

        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não encontrado");

        var reacao = await _reacaoRepository.GetByIdAsync(request.ReacaoId);
        NotFoundException.ThrowIfNull(reacao, $"Reação com o ID: '{request.PacienteId}' não encontrada");

        int rowsAffected = 0;

        var tipoReacao = Domain.Entities.PerguntaRespostaReacao.ConverterReacaoParaEnum(request.Reacao);
        if (tipoReacao == Shareable.Enums.ETipoReacaoResposta.None)
            rowsAffected = await _reacaoRepository.RemoveAsync(reacao);

        reacao.Update(tipoReacao);

        rowsAffected = await _reacaoRepository.UpdateAsync(reacao);

        return await Task.FromResult(rowsAffected > 0);
    }
}