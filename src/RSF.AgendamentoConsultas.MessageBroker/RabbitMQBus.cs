using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Domain.MessageBus.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RSF.AgendamentoConsultas.MessageBroker;

public sealed class RabbitMQBus: IEventBus
{
    private readonly RabbitMQConnection _connection;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Dictionary<string, List<Type>> _handlers = new();
    private readonly List<Type> _eventTypes = new();
    private readonly ILogger<RabbitMQBus> _logger;


    public RabbitMQBus(RabbitMQConnection connection, IServiceScopeFactory serviceScopeFactory, ILogger<RabbitMQBus> logger)
    {
        _connection = connection;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : Event
    {
        using var channel = _connection.CreateChannel();
        var eventName = @event.GetType().Name;
        channel.QueueDeclare(queue: eventName, durable: true, exclusive: false, autoDelete: false);

        _logger.LogInformation($"Queue with name '{eventName}' created");

        var message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "", routingKey: eventName, basicProperties: null, body: body);
    }

    public void Subcribe<TEvent, TEventHandler>()
        where TEvent : Event
        where TEventHandler : IEventHandler<TEvent>
    {
        var eventName = typeof(TEvent).Name;
        var handlerType = typeof(TEventHandler);

        if (!_eventTypes.Contains(typeof(TEvent)))
            _eventTypes.Add(typeof(TEvent));

        if (!_handlers.ContainsKey(eventName))
            _handlers.Add(eventName, new List<Type>());

        if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'", nameof(handlerType));

        _handlers[eventName].Add(handlerType);

        StartBasicConsume<TEvent>();
    }



    private void StartBasicConsume<TEvent>() where TEvent : Event
    {
        var channel = _connection.CreateChannel();
        var eventName = typeof(TEvent).Name;

        channel.QueueDeclare(queue: eventName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += Consumer_Received;

        channel.BasicConsume(queue: eventName, autoAck: true, consumer: consumer);
    }

    public async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        string eventName = e.RoutingKey;
        string message = Encoding.UTF8.GetString(e.Body.ToArray());

        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao processar evento: {ex.Message}");
        }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (_handlers.ContainsKey(eventName))
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var subscriptions = _handlers[eventName];
                foreach (var subscription in subscriptions)
                {
                    var handler = scope.ServiceProvider.GetService(subscription);
                    if (handler == null)
                        continue;

                    Type eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    Type concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}
