using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.Domain.MessageBus;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Domain.MessageBus.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Channels;

namespace RSF.AgendamentoConsultas.MessageBroker;

public sealed class RabbitMQService : IRabbitMQService, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ILogger<RabbitMQService> _logger;


    public RabbitMQService(IOptions<RabbitMQSettings> options, ILogger<RabbitMQService> logger)
    {
        _logger = logger;

        var factory = new ConnectionFactory
        {
            HostName = options.Value.Host,
            UserName = options.Value.UserName,
            Password = options.Value.Password,
            VirtualHost = options.Value.VirtualHost,
            Port = options.Value.Port
        };

        _connection = factory.CreateConnection("rabbitmq-client-publisher");
        _channel = _connection.CreateModel();

        _logger.LogInformation("RabbitMQ connection established.");
    }

    public void Publish<T>(T message, string routingKey, string exchange = "") where T : Event
    {
        try
        {
            _channel.QueueDeclare(queue: routingKey, durable: true, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(json);
            
            _channel.BasicPublish(exchange: exchange, routingKey: routingKey, body: body);

            _logger.LogInformation("Message published to {Exchange} with Routing Key: {RoutingKey}", exchange, routingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while publishing message.");
        }
    }

    public void Subscribe(string queueName, Func<string, Task> onMessageReceived)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            try
            {
                await onMessageReceived(message);
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing message from queue {QueueName}", queueName);
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

        _logger.LogInformation("Subscribed to queue {QueueName}", queueName);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}