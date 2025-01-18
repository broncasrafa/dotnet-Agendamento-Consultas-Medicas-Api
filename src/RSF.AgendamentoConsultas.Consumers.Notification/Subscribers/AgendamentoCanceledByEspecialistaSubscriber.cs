using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Notifications.Templates;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Domain.Notifications;
using RSF.AgendamentoConsultas.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class AgendamentoCanceledByEspecialistaSubscriber : IHostedService
{
    private readonly ILogger<AgendamentoCanceledByEspecialistaSubscriber> _logger;
    private readonly IOptions<RabbitMQSettings> _options;
    private readonly IServiceProvider _serviceProvider;

    public AgendamentoCanceledByEspecialistaSubscriber(
        ILogger<AgendamentoCanceledByEspecialistaSubscriber> logger,
        IOptions<RabbitMQSettings> options,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _options = options;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var queueName = _options.Value.AgendamentoCanceladoEspecialistaQueueName;

        _logger.LogInformation($"[{DateTime.Now}] Consuming message from queue '{queueName}'");

        using var scope = _serviceProvider.CreateScope();
        var _eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

        _eventBus.Subscribe(queueName: queueName, async (message) =>
        {
            using var scope = _serviceProvider.CreateScope();

            var mailSender = scope.ServiceProvider.GetRequiredService<AgendamentoCanceledByEspecialistaEmail>();

            var @event = JsonSerializer.Deserialize<AgendamentoCanceledByEspecialistaEvent>(message);

            await mailSender.SendEmailAsync(
                new MailTo(@event.PacienteNome, @event.PacienteEmail),
                @event.PacienteNome,
                @event.EspecialistaNome,
                @event.Especialidade,
                @event.DataConsulta,
                @event.HorarioConsulta,
                @event.LocalAtendimento);

            await Task.CompletedTask;
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}