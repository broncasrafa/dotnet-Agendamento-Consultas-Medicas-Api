using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class AgendamentoExpiredByPacienteSubscriber : RabbitMQConsumerBase
{
    private readonly ILogger<AgendamentoExpiredByPacienteSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public AgendamentoExpiredByPacienteSubscriber(ILogger<AgendamentoExpiredByPacienteSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider)
        : base(logger, options, options.Value.AgendamentoExpiradoPacienteQueueName)
    {
        _logger = logger;
        _queueName = options.Value.AgendamentoExpiradoPacienteQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

        using var scope = _serviceProvider.CreateScope();

        var mailSender = scope.ServiceProvider.GetRequiredService<AgendamentoExpiredByPacienteEmail>();

        var @event = JsonSerializer.Deserialize<AgendamentoExpiredByPacienteEvent>(message);

        await mailSender.SendEmailAsync(
            new MailTo(@event.PacienteNome, @event.PacienteEmail),
            @event.PacienteNome,
            @event.EspecialistaNome,
            @event.Especialidade,
            @event.DataConsulta,
            @event.HorarioConsulta,
            @event.LocalAtendimento,
            @event.NotaCancelamento);

        await Task.CompletedTask;
    }
}