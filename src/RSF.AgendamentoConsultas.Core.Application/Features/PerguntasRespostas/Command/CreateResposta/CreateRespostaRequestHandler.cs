using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.CreateResposta;

public class CreateRespostaRequestHandler : IRequestHandler<CreateRespostaRequest, Result<bool>>
{
    private readonly IBaseRepository<Domain.Entities.PerguntaResposta> _perguntaRespostaRepository;
    private readonly IPerguntaRepository _perguntaRepository;
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;

    public CreateRespostaRequestHandler(
        IBaseRepository<Domain.Entities.PerguntaResposta> perguntaRespostaRepository,
        IPerguntaRepository perguntaRepository,
        IEspecialistaRepository especialistaRepository,
        IPacienteRepository pacienteRepository,
        IEventBus eventBus,
        IConfiguration configuration)
    {
        _perguntaRespostaRepository = perguntaRespostaRepository;
        _perguntaRepository = perguntaRepository;
        _especialistaRepository = especialistaRepository;
        _pacienteRepository = pacienteRepository;
        _eventBus = eventBus;
        _configuration = configuration;
    }

    public async Task<Result<bool>> Handle(CreateRespostaRequest request, CancellationToken cancellationToken)
    {
        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var pergunta = await _perguntaRepository.GetByIdAsync(request.PerguntaId);
        NotFoundException.ThrowIfNull(pergunta, $"Pergunta com o ID: '{request.PerguntaId}' não encontrada");

        var paciente = await _pacienteRepository.GetByIdAsync(pergunta.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{pergunta.PacienteId}' não encontrado");

        var resposta = new Domain.Entities.PerguntaResposta(request.PerguntaId, request.EspecialistaId, request.Resposta);
        
        var rowsAffected = await _perguntaRespostaRepository.AddAsync(resposta);

        // envia a mensagem para a fila de respostas
        var @event = new RespostaCreatedEvent(pergunta.PacienteId, paciente.Nome, paciente.Email, pergunta.Especialidade.NomePlural, request.EspecialistaId, especialista.Nome, resposta.PerguntaRespostaId, request.Resposta);
        _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:RespostasPerguntasQueueName").Value);

        return await Task.FromResult(rowsAffected > 0);
    }
}