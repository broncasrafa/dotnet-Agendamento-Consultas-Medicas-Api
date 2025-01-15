using Microsoft.Extensions.Logging;
using RSF.AgendamentoConsultas.Domain.Events;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Domain.Notifications;
using RSF.AgendamentoConsultas.Notifications.Templates;
using RSF.AgendamentoConsultas.Shareable.Extensions;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Handlers;

public sealed class PerguntaCreatedEventHandler : IEventHandler<PerguntaCreatedEvent>
{
    private readonly ILogger<PerguntaCreatedEventHandler> _logger;
    private readonly PerguntaCreatedEmail _mailSender;
    private readonly IEspecialistaRepository _especialistaRepository;

    public PerguntaCreatedEventHandler(
        ILogger<PerguntaCreatedEventHandler> logger,
        PerguntaCreatedEmail mailSender,
        IEspecialistaRepository especialistaRepository)
    {
        _logger = logger;
        _mailSender = mailSender;
        _especialistaRepository = especialistaRepository;
    }

    public async Task Handle(PerguntaCreatedEvent @event)
    {
        _logger.LogInformation($"[{DateTime.Now}] Consumindo a mensagem da fila: 'PerguntaCreatedEvent' - {@event.ToJson(false)}");

        var especialistas = await _especialistaRepository.GetAllByEspecialidadeIdAsync(@event.EspecialidadeId);

        foreach (var esp in especialistas)
        {
            await _mailSender.SendEmailAsync(
                    to: new MailTo(esp.Nome, esp.Email),
                    pacienteNome: @event.PacienteNome,
                    especialidadeNome: @event.EspecialidadeNome,
                    pergunta: @event.Pergunta,
                    perguntaId: @event.PerguntaId,
                    especialistaId: esp.EspecialistaId,
                    especialistaNome: esp.Nome);
        }

        await Task.CompletedTask;
    }
}