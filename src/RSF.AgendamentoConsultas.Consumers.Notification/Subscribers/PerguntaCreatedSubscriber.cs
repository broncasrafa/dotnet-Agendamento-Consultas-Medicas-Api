using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Notifications;
using RSF.AgendamentoConsultas.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Shareable.Extensions;
using RSF.AgendamentoConsultas.Notifications.Templates;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

public sealed class PerguntaCreatedSubscriber : IHostedService
{
    private readonly IModel _channel;
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

        var connectionFactory = new ConnectionFactory
        {
            HostName = _options.Value.Host,
            UserName = _options.Value.UserName,
            Password = _options.Value.Password
        };

        var connection = connectionFactory.CreateConnection("rabbitmq-client-consumer");

        _channel = connection.CreateModel();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consumindo o evento...");

        _channel.QueueDeclare(queue: _options.Value.PerguntasQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(contentArray);

            var @event = JsonSerializer.Deserialize<PerguntaCreatedEvent>(message);

            Console.WriteLine($"Message received: {message}");

            _channel.BasicAck(eventArgs.DeliveryTag, false);

            _logger.LogInformation($"[{DateTime.Now}] Consumindo a mensagem da fila: 'PerguntaCreatedEvent' - {@event.ToJson(false)}");
            
            using var scope = _serviceProvider.CreateScope();
            var especialistaRepository = scope.ServiceProvider.GetRequiredService<IEspecialistaRepository>();
            var mailSender = scope.ServiceProvider.GetRequiredService<PerguntaCreatedEmail>();

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
        };

        _channel.BasicConsume(_options.Value.PerguntasQueueName, false, consumer);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

