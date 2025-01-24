using RSF.AgendamentoConsultas.Core.Domain.Models;

namespace RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

public class ChangePasswordCreatedEvent : Event
{
    public UsuarioAutenticadoModel Usuario { get; private set; }

    public ChangePasswordCreatedEvent(UsuarioAutenticadoModel usuario)
    {
        Usuario = usuario;
    }
}