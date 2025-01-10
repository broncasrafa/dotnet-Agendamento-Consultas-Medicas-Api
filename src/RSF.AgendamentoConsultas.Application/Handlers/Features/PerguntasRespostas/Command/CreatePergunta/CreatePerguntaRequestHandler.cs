using RSF.AgendamentoConsultas.Domain.Entities;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PerguntasRespostas.Command.CreatePergunta;

public class CreatePerguntaRequestHandler : IRequestHandler<CreatePerguntaRequest, Result<bool>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IBaseRepository<EspecialistaPergunta> _especialistaPerguntaRepository;

    public CreatePerguntaRequestHandler(
        IPacienteRepository pacienteRepository,
        IEspecialistaRepository especialistaRepository,
        IBaseRepository<EspecialistaPergunta> especialistaPerguntaRepository)
    {
        _pacienteRepository = pacienteRepository;
        _especialistaRepository = especialistaRepository;
        _especialistaPerguntaRepository = especialistaPerguntaRepository;
    }

    public async Task<Result<bool>> Handle(CreatePerguntaRequest request, CancellationToken cancellationToken)
    {
        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var pergunta = new EspecialistaPergunta(request.EspecialistaId, request.PacienteId, request.Titulo, request.Pergunta);
        await _especialistaPerguntaRepository.AddAsync(pergunta);
        var rowsAffected = await _especialistaPerguntaRepository.SaveChangesAsync();

        return await Task.FromResult(rowsAffected > 0);
    }
}