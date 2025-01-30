using System.Net;
using System.Security.Claims;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;

public class UnauthorizedRequestException(string message, HttpStatusCode statusCode = HttpStatusCode.Unauthorized) 
    : BaseException(message, statusCode)
{
    public static void ThrowIfNull(ClaimsPrincipal? user, string exceptionMessage)
    {
        if (user is null)
            throw new UnauthorizedRequestException(exceptionMessage);
    }

    public static void ThrowIfNotAuthenticated(bool isAuthenticated, string exceptionMessage)
    {
        if (!isAuthenticated)
            throw new UnauthorizedRequestException(exceptionMessage);
    }
}