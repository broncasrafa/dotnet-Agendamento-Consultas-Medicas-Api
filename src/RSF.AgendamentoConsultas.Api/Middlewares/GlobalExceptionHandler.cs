using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Shareable.Exceptions;

namespace RSF.AgendamentoConsultas.Api.Middlewares;

[ExcludeFromCodeCoverage]
internal class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problemDetails = new ProblemDetails();
        problemDetails.Instance = httpContext.Request.Path;
        problemDetails.Title = exception.Message;

        _logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

        if (exception is BaseException ex)
        {
            httpContext.Response.StatusCode = (int)ex.StatusCode;
            problemDetails.Title = ex.Message;
        }

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);

        return true;
    }
}