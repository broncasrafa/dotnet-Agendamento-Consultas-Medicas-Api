using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using OperationResult;
using MediatR;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddPerguntaEspecialista;

public class CreatePerguntaEspecialistaRequestHandler : IRequestHandler<CreatePerguntaEspecialistaRequest, Result<bool>>
{
    private readonly IEspecialistaPerguntaRepository _repository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IBaseRepository<Domain.Entities.Pergunta> _perguntaRepository;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;

    public CreatePerguntaEspecialistaRequestHandler(
        IEspecialistaPerguntaRepository repository, 
        IPacienteRepository pacienteRepository,
        IEspecialistaRepository especialistaRepository, 
        IBaseRepository<Domain.Entities.Pergunta> perguntaRepository, 
        IEventBus eventBus, 
        IConfiguration configuration)
    {
        _repository = repository;
        _pacienteRepository = pacienteRepository;
        _especialistaRepository = especialistaRepository;
        _perguntaRepository = perguntaRepository;
        _eventBus = eventBus;
        _configuration = configuration;
    }

    public async Task<Result<bool>> Handle(CreatePerguntaEspecialistaRequest request, CancellationToken cancellationToken)
    {
        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var especialidade = especialista.Especialidades.FirstOrDefault();
        var pergunta = new Domain.Entities.Pergunta(especialidade!.EspecialidadeId, request.PacienteId, request.Pergunta, request.TermosUsoPolitica);

        await _perguntaRepository.AddAsync(pergunta);

        var especialistaPergunta = new Domain.Entities.EspecialistaPergunta(request.EspecialistaId, pergunta.PerguntaId);

        var rowsAffected = await _repository.AddAsync(especialistaPergunta);
        if (rowsAffected > 0)
        {
            var @event = new PerguntaEspecialistaCreatedEvent(
                especialistaId: request.EspecialistaId,
                especialistaNome: especialista.Nome, 
                especialistaEmail: especialista.Email, 
                especialidadeNome: especialidade!.Especialidade.Nome,
                pacienteId: request.PacienteId, 
                pacienteNome: paciente.Nome, 
                pacienteEmail: paciente.Email, 
                perguntaId: pergunta.PerguntaId, 
                pergunta: request.Pergunta);

            _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:PerguntasEspecialistaQueueName").Value);
        }

        return await Task.FromResult(rowsAffected > 0);
    }
}