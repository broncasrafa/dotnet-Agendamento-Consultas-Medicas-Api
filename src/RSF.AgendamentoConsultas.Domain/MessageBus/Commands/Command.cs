using RSF.AgendamentoConsultas.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Domain.MessageBus.Commands;

public abstract class Command : Message
{
    public DateTime Timestamp { get; protected set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }
}