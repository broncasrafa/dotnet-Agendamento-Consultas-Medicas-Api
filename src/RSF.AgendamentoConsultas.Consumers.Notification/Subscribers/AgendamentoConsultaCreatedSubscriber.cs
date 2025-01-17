﻿using System.Text.Json;
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

public sealed class AgendamentoConsultaCreatedSubscriber : IHostedService
{
    //private readonly IModel _channel;
    private readonly ILogger<AgendamentoConsultaCreatedSubscriber> _logger;
    private readonly IOptions<RabbitMQSettings> _options;
    private readonly IServiceProvider _serviceProvider;

    public AgendamentoConsultaCreatedSubscriber(
        ILogger<AgendamentoConsultaCreatedSubscriber> logger,
        IOptions<RabbitMQSettings> options,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _options = options;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[{DateTime.Now}] Consuming message from queue '{_options.Value.AgendamentoQueueName}'");

        using var scope = _serviceProvider.CreateScope();
        var _eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

        _eventBus.Subscribe(_options.Value.AgendamentoQueueName, async (message) =>
        {
            using var scope = _serviceProvider.CreateScope();

            var mailSender = scope.ServiceProvider.GetRequiredService<AgendamentoCreatedEmail>();            

            var @event = JsonSerializer.Deserialize<AgendamentoConsultaCreatedEvent>(message);

            await mailSender.SendEmailAsync(new MailTo(@event.Especialista, @event.EspecialistaEmail),
                agendamentoConsultaId: @event.AgendamentoConsultaId,
                dataAgendamento: @event.DataAgendamento,
                tipoAgendamento: @event.TipoAgendamento,
                tipoConsulta: @event.TipoConsulta,
                especialidade: @event.Especialidade,
                especialista: @event.Especialista,
                especialistaEmail: @event.EspecialistaEmail,
                pacienteNome: @event.Paciente,
                convenioMedico: @event.ConvenioMedico,
                motivoConsulta: @event.MotivoConsulta,
                dataConsulta: @event.DataConsulta,
                horarioConsulta: @event.HorarioConsulta,
                primeiraVez: @event.PrimeiraVez,
                localAtendimentoNome: @event.LocalAtendimentoNome,
                localAtendimentoLogradouro: @event.LocalAtendimentoLogradouro,
                localAtendimentoComplemento: @event.LocalAtendimentoComplemento,
                localAtendimentoBairro: @event.LocalAtendimentoBairro,
                localAtendimentoCep: @event.LocalAtendimentoCep,
                localAtendimentoCidade: @event.LocalAtendimentoCidade,
                localAtendimentoEstado: @event.LocalAtendimentoEstado);

            await Task.CompletedTask;
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}