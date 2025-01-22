using System.Net;
using System.Diagnostics.CodeAnalysis;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;

[ExcludeFromCodeCoverage]
public class BaseException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
        : base(message)
    {
        StatusCode = statusCode;
    }
}