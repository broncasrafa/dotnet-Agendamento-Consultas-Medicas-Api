using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Domain.MessageBus.Commands;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;

namespace RSF.AgendamentoConsultas.MessageBroker;

public sealed class RabbitMQBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _eventTypes;
    private readonly IOptions<RabbitMQOptions> _options;

    public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory, IOptions<RabbitMQOptions> options)
    {
        _mediator = mediator;
        _serviceScopeFactory = serviceScopeFactory;

        _handlers = new Dictionary<string, List<Type>>();
        _eventTypes = new List<Type>();
        _options = options;
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : Event
    {
        var factory = new ConnectionFactory() { HostName = _options.Value.ConnectionString };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        var eventName = @event.GetType().Name;

        // declare a queue
        channel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        // create a message
        //var options = new JsonSerializerOptions { WriteIndented = true };
        //var message = JsonSerializer.Serialize<TEvent>(@event, options);
        var message = JsonConvert.SerializeObject(@event);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "", routingKey: eventName, basicProperties: null, body: body);
    }

    public Task SendCommand<TEvent>(TEvent command) where TEvent : Command
        => _mediator.Send(command);

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
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            DispatchConsumersAsync = true
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        var eventName = typeof(TEvent).Name;

        channel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += Consumer_Received;

        channel.BasicConsume(queue: eventName, autoAck: true, consumer: consumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
        string eventName = e.RoutingKey;
        string message = Encoding.UTF8.GetString(e.Body.ToArray());

        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(false);
        }
        catch (Exception)
        {
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
