using System.Net;
using System.Diagnostics.CodeAnalysis;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;

[ExcludeFromCodeCoverage]
public class JwtTokenGeneratorErrorException : BaseException
{
    public JwtTokenGeneratorErrorException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
        : base($"An error occurred while trying to generate JWT token: {message}", statusCode)
    {
    }
}