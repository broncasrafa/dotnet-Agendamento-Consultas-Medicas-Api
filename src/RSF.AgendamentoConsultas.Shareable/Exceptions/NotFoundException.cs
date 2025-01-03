using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace RSF.AgendamentoConsultas.Shareable.Exceptions;


[ExcludeFromCodeCoverage]
public class NotFoundException : BaseException
{
    public NotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound) 
        : base(message, statusCode)
    {
    }

    public static void ThrowIfNull(object? obj, string exceptionMessage)
    {
        if (obj is null)
            throw new NotFoundException(exceptionMessage);
    }
}