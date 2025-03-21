using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public sealed class PerguntaEspecialidadeCreatedSubscriber : RabbitMQConsumerBase
{
    private readonly ILogger<PerguntaEspecialidadeCreatedSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _queueName;

    public PerguntaEspecialidadeCreatedSubscriber(ILogger<PerguntaEspecialidadeCreatedSubscriber> logger, IOptions<RabbitMQSettings> options, IServiceProvider serviceProvider)
        : base(logger, options, options.Value.PerguntasEspecialidadeQueueName)
    {
        _logger = logger;
        _queueName = options.Value.PerguntasEspecialidadeQueueName;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consuming message from queue '{QueueName}'", _queueName);

        using var scope = _serviceProvider.CreateScope();

        var especialistaRepository = scope.ServiceProvider.GetRequiredService<IEspecialistaRepository>();

        var mailSender = scope.ServiceProvider.GetRequiredService<PerguntaEspecialidadeCreatedEmail>();

        var @event = JsonSerializer.Deserialize<PerguntaEspecialidadeCreatedEvent>(message);

        var especialistas = await especialistaRepository.GetAllByEspecialidadeIdAsync(@event.EspecialidadeId);
        var listTo = new List<MailTo>();
        listTo = especialistas.Select(esp => new MailTo(esp.Nome, esp.Email)).ToList();

        await mailSender.SendEmailAsync(
                toList: listTo,
                pacienteNome: @event.PacienteNome,
                especialidadeNome: @event.EspecialidadeNome,
                pergunta: @event.Pergunta,
                perguntaId: @event.PerguntaId);

        await Task.CompletedTask;
    }
}

