using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class EmailConfirmationSubscriber : RabbitMQConsumerBase
{
    private readonly ILogger<EmailConfirmationSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public EmailConfirmationSubscriber(ILogger<EmailConfirmationSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider)
        : base(logger, options, options.Value.EmailConfirmationQueueName)
    {
        _logger = logger;
        _queueName = options.Value.EmailConfirmationQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

        using var scope = _serviceProvider.CreateScope();

        var mailSender = scope.ServiceProvider.GetRequiredService<EmailConfirmationEmail>();

        var @event = JsonSerializer.Deserialize<EmailConfirmationCreatedEvent>(message);

        await mailSender.SendEmailAsync(
            to: new MailTo(@event.Usuario.Nome, @event.Usuario.Email),
            @event.Usuario.Nome,
            @event.Code,
            @event.EncodedCode);

        await Task.CompletedTask;
    }
}