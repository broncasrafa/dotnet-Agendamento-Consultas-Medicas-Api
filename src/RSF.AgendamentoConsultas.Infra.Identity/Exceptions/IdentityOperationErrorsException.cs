using System.Net;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;

namespace RSF.AgendamentoConsultas.Infra.Identity.Exceptions;

public class IdentityOperationErrorsException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
    : BaseException(message, statusCode)
{
    public static void ThrowIfErrors(string message)
    {
        if (!string.IsNullOrWhiteSpace(message))
            throw new IdentityOperationErrorsException(message);
    }
}