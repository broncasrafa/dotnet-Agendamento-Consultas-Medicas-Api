using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class RespostaCreatedSubscriber : RabbitMQConsumerBase
{
    private readonly ILogger<RespostaCreatedSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public RespostaCreatedSubscriber(ILogger<RespostaCreatedSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider)
        : base(logger, options, options.Value.RespostasQueueName)
    {
        _logger = logger;
        _queueName = options.Value.RespostasQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

        using var scope = _serviceProvider.CreateScope();

        var mailSender = scope.ServiceProvider.GetRequiredService<RespostaCreatedEmail>();

        var @event = JsonSerializer.Deserialize<RespostaCreatedEvent>(message);

        await mailSender.SendEmailAsync(
            new MailTo(@event.PacienteNome,
            @event.PacienteEmail),
            @event.PacienteNome,
            @event.EspecialistaNome,
            @event.EspecialidadeNome,
            @event.RespostaId,
            @event.Resposta);

        await Task.CompletedTask;
    }
}