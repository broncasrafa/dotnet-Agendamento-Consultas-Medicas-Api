using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class ForgotPasswordSubscriber : IHostedService
{
    private readonly ILogger<ForgotPasswordSubscriber> _logger;
    private readonly IOptions<RabbitMQSettings> _options;
    private readonly IServiceProvider _serviceProvider;

    public ForgotPasswordSubscriber(ILogger<ForgotPasswordSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _options = options;
        _serviceProvider = serviceProvider;
    }


    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var _eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

        var queueName = _options.Value.ForgotPasswordQueueName;

        _eventBus.Subscribe(queueName, async (message) =>
        {
            _logger.LogInformation($"[{DateTime.Now}] Consuming message from queue '{queueName}'");

            using var scope = _serviceProvider.CreateScope();
            
            var mailSender = scope.ServiceProvider.GetRequiredService<ForgotPasswordEmail>();

            var @event = JsonSerializer.Deserialize<ForgotPasswordCreatedEvent>(message);

            await mailSender.SendEmailAsync(
                to: new MailTo(@event.Usuario.Nome, @event.Usuario.Email), 
                @event.Usuario.Nome, 
                @event.Usuario.Email, 
                @event.ResetCode);

            await Task.CompletedTask;
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}