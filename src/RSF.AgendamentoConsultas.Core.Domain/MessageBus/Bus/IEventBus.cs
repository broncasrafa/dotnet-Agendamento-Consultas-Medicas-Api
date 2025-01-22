using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;

public interface IEventBus
{
    void Publish<T>(T message, string routingKey, string exchange = "") where T : Event;
    void Subscribe(string queueName, Func<string, Task> onMessageReceived);
}