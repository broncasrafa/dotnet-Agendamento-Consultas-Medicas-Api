using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class RespostaCreatedSubscriber : IHostedService
{
    private readonly ILogger<RespostaCreatedSubscriber> _logger;
    private readonly IOptions<RabbitMQSettings> _options;
    private readonly IServiceProvider _serviceProvider;

    public RespostaCreatedSubscriber(
        ILogger<RespostaCreatedSubscriber> logger,
        IOptions<RabbitMQSettings> options,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _options = options;
        _serviceProvider = serviceProvider;
    }



    public Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var _eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

        _eventBus.Subscribe(_options.Value.RespostasQueueName, async (message) =>
        {
            _logger.LogInformation($"[{DateTime.Now}] Consuming message from queue '{_options.Value.RespostasQueueName}'");

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
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}