using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Login;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Register;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterPaciente;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterEspecialista;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ForgotPassword;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ResetPassword;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmail;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmailResend;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ChangePassword;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.UpdateAuthenticatedUserInfo;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.DeleteAuthenticatedUserInfo;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Query.GetAuthenticatedUserInfo;
using MediatR;

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
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Realizar o registro de Administradores e Consultores na plataforma")
            .WithSummary("Realizar o registro de Administradores e Consultores na plataforma")
            .RequireAuthorization("OnlyAdmin")
            .WithOpenApi();

        routes.MapPost("/register/pacientes", static async (IMediator mediator, [FromBody] RegisterPacienteRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("RegisterPacientes")
            .Accepts<RegisterPacienteRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Realizar o registro de Pacientes na plataforma")
            .WithSummary("Realizar o registro de Pacientes na plataforma")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapPost("/register/especialistas", static async (IMediator mediator, [FromBody] RegisterEspecialistaRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("RegisterEspecialista")
            .Accepts<RegisterEspecialistaRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Realizar o registro de Especialistas na plataforma")
            .WithSummary("Realizar o registro de Especialistas na plataforma")
            .AllowAnonymous()
            .WithOpenApi();


        routes.MapPost("/forgot-password", static async (IMediator mediator, [FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("ForgotPassword")
            .Accepts<ForgotPasswordRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Receber o código de reset de senha")
            .WithSummary("Receber o código de reset de senha")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapPost("/reset-password", static async (IMediator mediator, [FromBody] ResetPasswordRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("ResetPassword")
            .Accepts<ResetPasswordRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Receber a troca de senha informando o código de reset de senha")
            .WithSummary("Receber a troca de senha informando o código de reset de senha")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapPost("/confirm-email", static async (IMediator mediator, [FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("ConfirmEmail")
            .Accepts<ConfirmEmailRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Confirmar o e-mail informando o código de confirmação")
            .WithSummary("Confirmar o e-mail informando o código de confirmação")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapPost("/resend-confirmation-email", static async (IMediator mediator, [FromBody] ConfirmEmailResendRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("ConfirmEmailResend")
            .Accepts<ConfirmEmailResendRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Reenviar o código de confirmação de e-mail")
            .WithSummary("Reenviar o código de confirmação de e-mail")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapPost("/change-password", static async (IMediator mediator, [FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("ChangePassword")
            .Accepts<ChangePasswordRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .WithDescription("Realizar a alteração de senha do usuário logado")
            .WithSummary("Realizar a alteração de senha do usuário logado")
            .RequireAuthorization()
            .WithOpenApi();
        #endregion

        #region [ GET ]
        routes.MapGet("/manage/info", static async (IMediator mediator, CancellationToken cancellationToken)
            => await mediator.SendCommand(new SelectAuthenticatedUserInfoRequest(), cancellationToken: cancellationToken))
            .WithName("GetManageUserInfo")
            .Produces<ApiResponse<AuthenticatedUserResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .WithDescription("Obter os dados do usuário logado na plataforma")
            .WithSummary("Obter os dados do usuário logado na plataforma")
            .RequireAuthorization()
            .WithOpenApi();
        #endregion

        #region [ PUT ]
        routes.MapPut("/manage/info", static async (IMediator mediator, [FromBody] UpdateAuthenticatedUserInfoRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("UpdateManageUserInfo")
            .Accepts<UpdateAuthenticatedUserInfoRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .WithDescription("Atualizar os dados do usuário logado na plataforma")
            .WithSummary("Atualizar os dados do usuário logado na plataforma")
            .RequireAuthorization()
            .WithOpenApi();
        #endregion

        #region [ DELETE ]
        routes.MapDelete("/manage/info", static async (IMediator mediator, CancellationToken cancellationToken)
            => await mediator.SendCommand(new DeleteAuthenticatedUserInfoRequest(), cancellationToken: cancellationToken))
            .WithName("DeleteManageUserInfo")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .WithDescription("Deleta os dados do usuário logado na plataforma")
            .WithSummary("Deleta os dados do usuário logado na plataforma")
            .RequireAuthorization()
            .WithOpenApi();
        #endregion

        return routes;
    }
}