using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Domain.Events;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Command.CreateResposta;

public class CreateRespostaRequestHandler : IRequestHandler<CreateRespostaRequest, Result<bool>>
{
    private readonly IBaseRepository<Domain.Entities.PerguntaResposta> _perguntaRespostaRepository;
    private readonly IBaseRepository<Domain.Entities.Pergunta> _perguntaRepository;
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEventBus _eventBus;

    public CreateRespostaRequestHandler(
        IBaseRepository<Domain.Entities.PerguntaResposta> perguntaRespostaRepository,
        IBaseRepository<Domain.Entities.Pergunta> perguntaRepository,
        IEspecialistaRepository especialistaRepository,
        IPacienteRepository pacienteRepository,
        IEventBus eventBus)
    {
        _perguntaRespostaRepository = perguntaRespostaRepository;
        _perguntaRepository = perguntaRepository;
        _especialistaRepository = especialistaRepository;
        _pacienteRepository = pacienteRepository;
        _eventBus = eventBus;
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
        _eventBus.Publish(new RespostaCreatedEvent(pergunta.PacienteId, paciente.Nome, paciente.Email, request.EspecialistaId, especialista.Nome, request.Resposta));

        return await Task.FromResult(rowsAffected > 0);
    }
}