using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RabbitMQ.Client;

namespace RSF.AgendamentoConsultas.Infra.MessageBroker;

public sealed class RabbitMQService : IEventBus
{
    private readonly IModel _channel;
    private readonly ILogger<RabbitMQService> _logger;
    private readonly RabbitMQConnection _connection;

    public RabbitMQService(IOptions<RabbitMQSettings> options, ILogger<RabbitMQService> logger, RabbitMQConnection connection)
    {
        _logger = logger;
        _connection = connection;
        _channel = _connection.CreateChannel();
    }

    public void Publish<T>(T message, string routingKey, string exchange = "") where T : Event
    {
        try
        {
            _channel.QueueDeclare(queue: routingKey, durable: true, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(json);
            
            _channel.BasicPublish(exchange: exchange, routingKey: routingKey, body: body);

            _logger.LogInformation("Message published to Routing Key: {RoutingKey}", routingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while publishing message.");
        }
    }
}