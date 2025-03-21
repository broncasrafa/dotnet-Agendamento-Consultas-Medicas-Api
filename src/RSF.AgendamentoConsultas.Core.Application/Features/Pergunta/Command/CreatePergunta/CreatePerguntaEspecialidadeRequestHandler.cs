using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Command.CreatePergunta;

public class CreatePerguntaEspecialidadeRequestHandler : IRequestHandler<CreatePerguntaEspecialidadeRequest, Result<bool>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEspecialidadeRepository _especialidadeRepository;
    private readonly IBaseRepository<Domain.Entities.Pergunta> _perguntaRepository;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;

    public CreatePerguntaEspecialidadeRequestHandler(
        IPacienteRepository pacienteRepository,
        IEspecialidadeRepository especialidadeRepository,
        IBaseRepository<Domain.Entities.Pergunta> especialistaPerguntaRepository,
        IEventBus eventBus,
        IConfiguration configuration)
    {
        _pacienteRepository = pacienteRepository;
        _especialidadeRepository = especialidadeRepository;
        _perguntaRepository = especialistaPerguntaRepository;
        _eventBus = eventBus;
        _configuration = configuration;
    }

    public async Task<Result<bool>> Handle(CreatePerguntaEspecialidadeRequest request, CancellationToken cancellationToken)
    {
        var especialidade = await _especialidadeRepository.GetByIdAsync(request.EspecialidadeId);
        NotFoundException.ThrowIfNull(especialidade, $"Especialidade com o ID: '{request.EspecialidadeId}' não encontrada");

        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var pergunta = new Domain.Entities.Pergunta(request.EspecialidadeId, request.PacienteId, request.Pergunta, request.TermosUsoPolitica);

        var rowsAffected = await _perguntaRepository.AddAsync(pergunta);
        if (rowsAffected > 0)
        {
            // envia mensagem para a fila de Perguntas criadas
            var @event = new PerguntaEspecialidadeCreatedEvent(
                request.EspecialidadeId, 
                especialidade.NomePlural, 
                request.PacienteId, 
                paciente.Nome, 
                paciente.Email, 
                pergunta.PerguntaId, 
                request.Pergunta);

            _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:PerguntasEspecialidadeQueueName").Value);
        }
                
        return await Task.FromResult(rowsAffected > 0);
    }
}