using RSF.AgendamentoConsultas.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Domain.MessageBus.Commands;

namespace RSF.AgendamentoConsultas.Domain.MessageBus.Bus;

public interface IEventBus
{
    /// <summary>
    /// Enviar comandos para vários lugares através do bus e que deve ser do tipo Command.
    /// </summary>
    /// <typeparam name="T">tipo generico</typeparam>
    /// <param name="command">comando a ser enviado</param>
    Task SendCommand<TEvent>(TEvent command) where TEvent : Command;

    /// <summary>
    /// Publica qualquer tipo de evento e o evento tem que ser do tipo Event
    /// </summary>
    /// <typeparam name="T">tipo generico<</typeparam>
    /// <param name="event">evento para ser publicado</param>
    void Publish<TEvent>(TEvent @event) where TEvent : Event;


    /// <summary>
    /// Se inscrever nos eventos publicados. os eventos são do tipo Type e os manipuladores de eventos do tipo EventHandler.
    /// </summary>
    /// <typeparam name="T">tipo generico<</typeparam>
    /// <typeparam name="THandler">tipo generico de manipulador de eventos</typeparam>
    void Subcribe<TEvent, TEventHandler>()
        where TEvent : Event
        where TEventHandler : IEventHandler<TEvent>;
}