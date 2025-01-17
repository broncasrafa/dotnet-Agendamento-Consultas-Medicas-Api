using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Notifications;
using RSF.AgendamentoConsultas.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Notifications.Templates;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;

namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public sealed class PerguntaCreatedSubscriber : IHostedService
{
    private readonly ILogger<PerguntaCreatedSubscriber> _logger;
    private readonly IOptions<RabbitMQSettings> _options;
    private readonly IServiceProvider _serviceProvider;

    public PerguntaCreatedSubscriber(
        ILogger<PerguntaCreatedSubscriber> logger, 
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

        _eventBus.Subscribe(_options.Value.PerguntasQueueName, async (message) =>
        {
            _logger.LogInformation($"[{DateTime.Now}] Consuming message from queue '{_options.Value.PerguntasQueueName}'");

            using var scope = _serviceProvider.CreateScope();

            var especialistaRepository = scope.ServiceProvider.GetRequiredService<IEspecialistaRepository>();
            var mailSender = scope.ServiceProvider.GetRequiredService<PerguntaCreatedEmail>();

            var @event = JsonSerializer.Deserialize<PerguntaCreatedEvent>(message);

            var especialistas = await especialistaRepository.GetAllByEspecialidadeIdAsync(@event.EspecialidadeId);

            foreach (var esp in especialistas)
            {
                await mailSender.SendEmailAsync(
                    to: new MailTo(esp.Nome, esp.Email),
                    pacienteNome: @event.PacienteNome,
                    especialidadeNome: @event.EspecialidadeNome,
                    pergunta: @event.Pergunta,
                    perguntaId: @event.PerguntaId,
                    especialistaId: esp.EspecialistaId,
                    especialistaNome: esp.Nome);
            }

            await Task.CompletedTask;
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

