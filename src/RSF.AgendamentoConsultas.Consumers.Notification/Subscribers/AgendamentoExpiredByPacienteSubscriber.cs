﻿using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;


namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public class AgendamentoExpiredByPacienteSubscriber : IHostedService
{
    private readonly ILogger<AgendamentoExpiredByPacienteSubscriber> _logger;
    private readonly IOptions<RabbitMQSettings> _options;
    private readonly IServiceProvider _serviceProvider;

    public AgendamentoExpiredByPacienteSubscriber(
        ILogger<AgendamentoExpiredByPacienteSubscriber> logger, 
        IOptions<RabbitMQSettings> options, 
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _options = options;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var queueName = _options.Value.AgendamentoExpiradoPacienteQueueName;

        _logger.LogInformation($"[{DateTime.Now}] Consuming message from queue '{queueName}'");

        using var scope = _serviceProvider.CreateScope();
        var _eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

        _eventBus.Subscribe(queueName: queueName, async (message) =>
        {
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
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}