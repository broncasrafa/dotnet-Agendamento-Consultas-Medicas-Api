using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RSF.AgendamentoConsultas.Consumers.Notification;

public abstract class RabbitMQConsumerBase : BackgroundService
{
    private readonly ILogger _logger;
    private readonly string _queueName;
    private IModel _channel;

    protected RabbitMQConsumerBase(ILogger logger, IOptions<RabbitMQSettings> options, string queueName)
    {
        _logger = logger;
        _queueName = queueName;

        if (string.IsNullOrWhiteSpace(options.Value.Host))
            throw new InvalidOperationException("A configuração 'Host' do RabbitMQ não foi fornecida corretamente.");

        var factory = new ConnectionFactory
        {
            HostName = options.Value.Host,
            UserName = options.Value.UserName,
            Password = options.Value.Password,
            VirtualHost = options.Value.VirtualHost,
            Port = options.Value.Port
        };

        var connection = factory.CreateConnection(options.Value.ClientProviderName);
        _channel = connection.CreateModel();

        _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Subscribed to queue {QueueName}", _queueName);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation("Mensagem recebida na fila '{QueueName}'", _queueName);

            try
            {
                await ProcessMessageAsync(message, stoppingToken);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar mensagem da fila '{QueueName}'", _queueName);
                // Possível reprocessamento ou log
                _channel.BasicNack(eventArgs.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Close();
        base.Dispose();
        GC.SuppressFinalize(this);
    }


    protected abstract Task ProcessMessageAsync(string message, CancellationToken cancellationToken);

}