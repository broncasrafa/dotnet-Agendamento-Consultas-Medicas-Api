using RSF.AgendamentoConsultas.Core.Domain.Models;

namespace RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

public class ResetPasswordCreatedEvent : Event
{
    public UsuarioAutenticadoModel Usuario { get; private set; }

    public ResetPasswordCreatedEvent(UsuarioAutenticadoModel usuario)
    {
        Usuario = usuario;
    }
}