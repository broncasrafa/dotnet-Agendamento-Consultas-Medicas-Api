using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public sealed class PerguntaEspecialistaCreatedSubscriber : RabbitMQConsumerBase
{
    private readonly ILogger<PerguntaEspecialistaCreatedSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public PerguntaEspecialistaCreatedSubscriber(ILogger<PerguntaEspecialistaCreatedSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider) 
        : base(logger, options, options.Value.PerguntasEspecialistaQueueName)
    {
        _logger = logger;
        _queueName = options.Value.PerguntasEspecialistaQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

        using var scope = _serviceProvider.CreateScope();

        var mailSender = scope.ServiceProvider.GetRequiredService<PerguntaEspecialistaCreatedEmail>();

        var @event = JsonSerializer.Deserialize<PerguntaEspecialistaCreatedEvent>(message);

        await mailSender.SendEmailAsync(
            new MailTo(@event.EspecialistaNome, @event.EspecialistaEmail),
            @event.PacienteNome,
            @event.EspecialistaNome,
            @event.EspecialidadeNome,
            @event.Pergunta,
            @event.PerguntaId);

        await Task.CompletedTask;
    }
}