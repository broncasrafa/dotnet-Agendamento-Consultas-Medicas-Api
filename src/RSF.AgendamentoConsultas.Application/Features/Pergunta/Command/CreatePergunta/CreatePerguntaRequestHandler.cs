using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.Command.CreatePergunta;

public class CreatePerguntaRequestHandler : IRequestHandler<CreatePerguntaRequest, Result<bool>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEspecialidadeRepository _especialidadeRepository;
    private readonly IBaseRepository<Domain.Entities.Pergunta> _perguntaRepository;
    //private readonly IEventBus _eventBus;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;

    public CreatePerguntaRequestHandler(
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

    public async Task<Result<bool>> Handle(CreatePerguntaRequest request, CancellationToken cancellationToken)
    {
        var especialidade = await _especialidadeRepository.GetByIdAsync(request.EspecialidadeId);
        NotFoundException.ThrowIfNull(especialidade, $"Especialidade com o ID: '{request.EspecialidadeId}' não encontrada");

        var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var pergunta = new Domain.Entities.Pergunta(request.EspecialidadeId, request.PacienteId, request.Pergunta, request.TermosUsoPolitica);

        var rowsAffected = await _perguntaRepository.AddAsync(pergunta);

        // envia mensagem para a fila de Perguntas criadas
        var @event = new PerguntaCreatedEvent(request.EspecialidadeId, especialidade.NomePlural, request.PacienteId, paciente.Nome, paciente.Email, pergunta.PerguntaId, request.Pergunta);
        _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:PerguntasQueueName").Value);
        
        return await Task.FromResult(rowsAffected > 0);
    }
}