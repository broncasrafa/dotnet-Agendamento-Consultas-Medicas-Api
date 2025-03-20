using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;


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

    public static void ThrowIfNotExists(bool exists, string exceptionMessage)
    {
        if (!exists)
            throw new NotFoundException(exceptionMessage);
    }
}