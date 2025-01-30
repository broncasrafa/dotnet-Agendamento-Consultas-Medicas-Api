using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class AgendamentoCanceledByEspecialistaSubscriber : RabbitMQConsumerBase
{
    private readonly ILogger<AgendamentoCanceledByEspecialistaSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public AgendamentoCanceledByEspecialistaSubscriber(
        ILogger<AgendamentoCanceledByEspecialistaSubscriber> logger,
        IOptions<RabbitMQSettings> options,
        IServiceProvider serviceProvider) : base(logger, options, options.Value.AgendamentoCanceladoEspecialistaQueueName)
    {
        _logger = logger;
        _queueName = options.Value.AgendamentoCanceladoEspecialistaQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

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
    }    
}