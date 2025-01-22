using Microsoft.AspNetCore.Http;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;

public static class HttpContextExtensions
{
    public static string? GetBearerToken(this HttpContext httpContext)
    {
        var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        var bearerToken = (authorizationHeader is null) ? null : authorizationHeader.Split(" ").LastOrDefault();
        return bearerToken;
    }
}