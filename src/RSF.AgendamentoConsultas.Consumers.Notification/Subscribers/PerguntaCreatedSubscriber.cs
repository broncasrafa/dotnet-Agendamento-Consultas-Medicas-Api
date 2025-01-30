using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public sealed class PerguntaCreatedSubscriber : RabbitMQConsumerBase
{
    private readonly ILogger<PerguntaCreatedSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public PerguntaCreatedSubscriber(ILogger<PerguntaCreatedSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider)
        : base(logger, options, options.Value.PerguntasQueueName)
    {
        _logger = logger;
        _queueName = options.Value.PerguntasQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

        using var scope = _serviceProvider.CreateScope();

        var especialistaRepository = scope.ServiceProvider.GetRequiredService<IEspecialistaRepository>();
        var mailSender = scope.ServiceProvider.GetRequiredService<PerguntaCreatedEmail>();

        var @event = JsonSerializer.Deserialize<PerguntaCreatedEvent>(message);

        var especialistas = await especialistaRepository.GetAllByEspecialidadeIdAsync(@event.EspecialidadeId);

        foreach (var esp in especialistas)
        {
            await mailSender.SendEmailAsync(
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

