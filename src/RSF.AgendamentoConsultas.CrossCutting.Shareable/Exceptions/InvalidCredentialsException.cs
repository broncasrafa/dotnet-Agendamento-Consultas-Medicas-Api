using System.Net;
using System.Diagnostics.CodeAnalysis;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;

[ExcludeFromCodeCoverage]
public class InvalidCredentialsException(HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
    : BaseException("Dados das credenciais inválidos", statusCode)
{
    public static void ThrowIfNotValid(bool isValid)
    {
        if (!isValid)
            throw new InvalidCredentialsException();
    }
}