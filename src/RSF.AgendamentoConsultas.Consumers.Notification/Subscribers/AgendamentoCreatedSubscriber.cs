using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public sealed class AgendamentoCreatedSubscriber : RabbitMQConsumerBase
{    
    private readonly ILogger<AgendamentoCreatedSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public AgendamentoCreatedSubscriber(ILogger<AgendamentoCreatedSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider)
        : base(logger, options, options.Value.AgendamentoQueueName)
    {
        _logger = logger;
        _queueName = options.Value.AgendamentoQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

        using var scope = _serviceProvider.CreateScope();

        var mailSender = scope.ServiceProvider.GetRequiredService<AgendamentoCreatedEmail>();

        var @event = JsonSerializer.Deserialize<AgendamentoCreatedEvent>(message);

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
    }
}