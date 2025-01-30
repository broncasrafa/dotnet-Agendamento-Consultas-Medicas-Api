using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class AgendamentoExpiredByEspecialistaSubscriber : RabbitMQConsumerBase
{
    private readonly ILogger<AgendamentoExpiredByEspecialistaSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public AgendamentoExpiredByEspecialistaSubscriber(ILogger<AgendamentoExpiredByEspecialistaSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider)
        : base(logger, options, options.Value.AgendamentoExpiradoEspecialistaQueueName)
    {
        _logger = logger;
        _queueName = options.Value.AgendamentoExpiradoEspecialistaQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

        using var scope = _serviceProvider.CreateScope();

        var mailSender = scope.ServiceProvider.GetRequiredService<AgendamentoExpiredByEspecialistaEmail>();

        var @event = JsonSerializer.Deserialize<AgendamentoExpiredByEspecialistaEvent>(message);

        await mailSender.SendEmailAsync(
            new MailTo(@event.EspecialistaNome, @event.EspecialistaEmail),
            @event.PacienteNome,
            @event.EspecialistaNome,
            @event.Especialidade,
            @event.DataConsulta,
            @event.HorarioConsulta,
            @event.LocalAtendimento);

        await Task.CompletedTask;
    }
}