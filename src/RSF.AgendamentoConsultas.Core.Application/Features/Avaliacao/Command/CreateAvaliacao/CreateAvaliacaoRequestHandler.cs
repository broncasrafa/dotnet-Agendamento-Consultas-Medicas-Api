using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Command.CreateAvaliacao;

public class CreateAvaliacaoRequestHandler : IRequestHandler<CreateAvaliacaoRequest, Result<bool>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IBaseRepository<EspecialistaAvaliacao> _especialistaAvaliacaoRepository;

    public CreateAvaliacaoRequestHandler(
        IPacienteRepository pacienteRepository,
        IEspecialistaRepository especialistaRepository,
        IBaseRepository<EspecialistaAvaliacao> especialistaAvaliacaoRepository)
    {
        _pacienteRepository = pacienteRepository;
        _especialistaRepository = especialistaRepository;
        _especialistaAvaliacaoRepository = especialistaAvaliacaoRepository;
    }

    public async Task<Result<bool>> Handle(CreateAvaliacaoRequest request, CancellationToken cancellationToken)
    {
        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var avaliacao = new EspecialistaAvaliacao(request.EspecialistaId, request.PacienteId, request.Feedback, request.Score);

        var rowsAffected = await _especialistaAvaliacaoRepository.AddAsync(avaliacao);

        return await Task.FromResult(rowsAffected > 0);
    }
}