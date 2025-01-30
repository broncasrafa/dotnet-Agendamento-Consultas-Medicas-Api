using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;

public static class HttpContextExtensions
{
    public static string? GetBearerToken(this HttpContext httpContext)
    {
        UnauthorizedRequestException.ThrowIfNotAuthenticated(httpContext.User.Identity?.IsAuthenticated ?? false, "Usuário não autenticado na plataforma");

        var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        var bearerToken = (authorizationHeader is null) ? null : authorizationHeader.Split(" ").LastOrDefault();
        return bearerToken;
    }

    public static string? GetUserIdFromClaims(this HttpContext httpContext)
    {
        UnauthorizedRequestException.ThrowIfNotAuthenticated(httpContext.User.Identity?.IsAuthenticated ?? false, "Usuário não autenticado na plataforma");

        // Tenta pegar a claim "uid"
        return httpContext.User.FindFirst("uid")?.Value;
    }

    public static int GetUserResourceIdFromClaims(this HttpContext httpContext)
    {
        UnauthorizedRequestException.ThrowIfNotAuthenticated(httpContext.User.Identity?.IsAuthenticated ?? false, "Usuário não autenticado na plataforma");

        // Tenta pegar a claim "id"
        var id = httpContext.User.FindFirst("id")?.Value is null 
            ? 0 
            : Convert.ToInt32(httpContext.User.FindFirst("id").Value);
        return id;
    }

    public static string? GetUserRole(this HttpContext httpContext)
    {
        UnauthorizedRequestException.ThrowIfNotAuthenticated(httpContext.User.Identity?.IsAuthenticated ?? false, "Usuário não autenticado na plataforma");

        // Tenta obter a claim de Role
        return httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
    }

    public static void ValidatePermissions(HttpContext httpContext, int id, ETipoPerfilAcesso perfilUsuario)
    {
        var resourceUserId = httpContext.GetUserResourceIdFromClaims();
        var userRole = httpContext.GetUserRole();
        var isInvalid = userRole == perfilUsuario.ToString() && resourceUserId != id;
        InputRequestDataInvalidException.ThrowIfResourceIdInvalid(isInvalid, "Usuário não tem permissão para atualizar este recurso");
    }
}