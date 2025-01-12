using RSF.AgendamentoConsultas.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Domain.MessageBus.Bus;

public interface IEventHandler
{
}

public interface IEventHandler<in TEvent> : IEventHandler where TEvent : Event
{
    Task Handle(TEvent @event);
}