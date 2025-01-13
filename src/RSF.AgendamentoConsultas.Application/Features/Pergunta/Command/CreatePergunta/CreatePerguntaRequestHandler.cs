using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Domain.Events;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.Command.CreatePergunta;

public class CreatePerguntaRequestHandler : IRequestHandler<CreatePerguntaRequest, Result<bool>>
{
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEspecialidadeRepository _especialidadeRepository;
    private readonly IBaseRepository<Domain.Entities.Pergunta> _perguntaRepository;
    private readonly IEventBus _eventBus;

    public CreatePerguntaRequestHandler(
        IPacienteRepository pacienteRepository,
        IEspecialidadeRepository especialidadeRepository,
        IBaseRepository<Domain.Entities.Pergunta> especialistaPerguntaRepository,
        IEventBus eventBus)
    {
        _pacienteRepository = pacienteRepository;
        _especialidadeRepository = especialidadeRepository;
        _perguntaRepository = especialistaPerguntaRepository;
        _eventBus = eventBus;
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
        _eventBus.Publish(new PerguntaCreatedEvent(request.EspecialidadeId, request.PacienteId, paciente.Email, pergunta.PerguntaId, request.Pergunta));
        
        return await Task.FromResult(rowsAffected > 0);
    }
}