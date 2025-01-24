using RSF.AgendamentoConsultas.Core.Domain.Models;

namespace RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

public class ForgotPasswordCreatedEvent : Event
{
    public UsuarioAutenticadoModel Usuario { get; private set; }
    public string ResetCode { get; private set; }

    public ForgotPasswordCreatedEvent(UsuarioAutenticadoModel usuario, string resetCode)
    {
        Usuario = usuario;
        ResetCode = resetCode;
    }
}