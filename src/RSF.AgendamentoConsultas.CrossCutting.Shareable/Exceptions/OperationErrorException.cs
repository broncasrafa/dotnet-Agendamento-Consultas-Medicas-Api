using System.Net;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;

public class OperationErrorException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
    : BaseException(message, statusCode)
{
}