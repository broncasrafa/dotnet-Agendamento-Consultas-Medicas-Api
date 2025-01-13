namespace RSF.AgendamentoConsultas.Domain.MessageBus.Events;

public abstract class Event
{
    public DateTime CreatedAt { get; protected set; }

    protected Event()
    {
        CreatedAt = DateTime.Now;
    }
}