using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using RSF.AgendamentoConsultas.Core.Domain.Models;

namespace RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

public class EmailConfirmationCreatedEvent(UsuarioAutenticadoModel usuario, string code) : Event
{
    public UsuarioAutenticadoModel Usuario { get; private set; } = usuario;
    public string Code { get; private set; } = code;
    public string EncodedCode
    {
        get
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(Code, nameof(Code));
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(Code));
        }
    }
}