using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace RSF.AgendamentoConsultas.Api.Middlewares;

[ExcludeFromCodeCoverage]
internal class AuthenticationExceptionHandler
{
    public static async Task HandleAuthError(HttpContext httpContext, int statusCode)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Instance = httpContext.Request.Path,
            Title = statusCode switch
            {
                StatusCodes.Status401Unauthorized => "Você não está autenticado.",
                StatusCodes.Status403Forbidden => "Acesso negado.",
                _ => "Erro de autenticação."
            },
            Detail = statusCode switch
            {
                StatusCodes.Status401Unauthorized => "Envie um token válido no header da requisição de autorização.",
                StatusCodes.Status403Forbidden => "Você não tem permissão para acessar este recurso.",
                _ => "Por favor contate o suporte para mais informações."
            }
        };

        // Log opcional
        var logger = httpContext.RequestServices.GetRequiredService<ILogger<AuthenticationExceptionHandler>>();
        logger.LogError("Erro de autenticação ({StatusCode}): {Path}", statusCode, httpContext.Request.Path);

        await httpContext.Response.WriteAsJsonAsync(problemDetails).ConfigureAwait(false);
    }
}