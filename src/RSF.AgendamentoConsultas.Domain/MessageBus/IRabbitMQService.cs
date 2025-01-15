using RSF.AgendamentoConsultas.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Domain.MessageBus;

public interface IRabbitMQService
{
    void Publish<T>(T message, string routingKey, string exchange = "") where T : Event;
    void Subscribe(string queueName, Func<string, Task> onMessageReceived);
}