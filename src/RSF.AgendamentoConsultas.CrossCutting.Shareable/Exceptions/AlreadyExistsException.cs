using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;


[ExcludeFromCodeCoverage]
public class AlreadyExistsException : BaseException
{
    public AlreadyExistsException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        : base(message, statusCode)
    {
    }

    public static void ThrowIfExists(object? obj, string exceptionMessage)
    {
        if (obj is not null)
            throw new AlreadyExistsException(exceptionMessage);
    }
}