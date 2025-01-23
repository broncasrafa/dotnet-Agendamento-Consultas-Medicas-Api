using MediatR;
using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Login;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Register;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/account").WithTags("Account");

        #region [ POST ]
        routes.MapPost("/login", static async (IMediator mediator, [FromBody] LoginRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("LoginUsuario")
            .Accepts<LoginRequest>("application/json")
            .Produces<ApiResponse<AuthenticatedUserResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Realizar o login do usuário na plataforma")
            .WithSummary("Realizar o login do usuário na plataforma")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapPost("/register", static async (IMediator mediator, [FromBody] RegisterRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("RegisterAdminConsultor")
            .Accepts<RegisterRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .WithDescription("Realizar o registro de Administradores e Consultores na plataforma")
            .WithSummary("Realizar o registro de Administradores e Consultores na plataforma")
            .RequireAuthorization(policy => policy.RequireRole(ETipoPerfilAcesso.Administrador.ToString()))
            .WithOpenApi();
        #endregion

        return routes;
    }
}