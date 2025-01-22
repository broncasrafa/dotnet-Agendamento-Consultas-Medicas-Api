namespace RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

public abstract class Event
{
    public DateTime CreatedAt { get; protected set; }

    protected Event()
    {
        CreatedAt = DateTime.Now;
    }
}