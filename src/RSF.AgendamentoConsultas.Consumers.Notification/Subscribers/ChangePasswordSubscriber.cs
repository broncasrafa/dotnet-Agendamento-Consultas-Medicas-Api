using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class ChangePasswordSubscriber : RabbitMQConsumerBase
{
    private readonly ILogger<ChangePasswordSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public ChangePasswordSubscriber(ILogger<ChangePasswordSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider)
        : base(logger, options, options.Value.ChangePasswordQueueName)
    {
        _logger = logger;
        _queueName = options.Value.ChangePasswordQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

        using var scope = _serviceProvider.CreateScope();

        var mailSender = scope.ServiceProvider.GetRequiredService<ChangePasswordEmail>();

        var @event = JsonSerializer.Deserialize<ChangePasswordCreatedEvent>(message);

        await mailSender.SendEmailAsync(to: new MailTo(@event.Usuario.Nome, @event.Usuario.Email), @event.Usuario.Nome);

        await Task.CompletedTask;
    }
}