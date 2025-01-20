using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Application.Features.Agendamento.Responses;
using RSF.AgendamentoConsultas.Application.Features.Agendamento.Query.GetAgendamentoById;
using RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.CreateAgendamento;
using RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.CancelAgendamentoByPaciente;
using RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.CancelAgendamentoByEspecialista;
using RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.ConfirmAgendamentoByPaciente;
using RSF.AgendamentoConsultas.Application.Features.Agendamento.Command.ConfirmAgendamentoByEspecialista;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class AgendamentoConsultaEndpoints
{
    public static IEndpointRouteBuilder MapAgendamentoConsultaEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/agendamentos").WithTags("Agendamento");

        #region [ POST ]
        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreateAgendamentoRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreateAgendamentoConsulta")
            .Accepts<CreateAgendamentoRequest>("application/json")
            .Produces<ApiResponse<int>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar um agendamento de uma consulta")
            .WithSummary("Adicionar um agendamento de uma consulta")
            .WithOpenApi();
        #endregion

        #region [ PUT ]
        routes.MapPut("/cancelamento/paciente", static async (IMediator mediator, [FromBody] CancelAgendamentoByPacienteRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CancelamentoAgendamentoConsultaPaciente")
            .Accepts<CancelAgendamentoByPacienteRequest>("application/json")
            .Produces<ApiResponse<int>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Cancelamento do agendamento de uma consulta pelo paciente")
            .WithSummary("Cancelamento do agendamento de uma consulta pelo paciente")
            .WithOpenApi();

        routes.MapPut("/cancelamento/especialista", static async (IMediator mediator, [FromBody] CancelAgendamentoByEspecialistaRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CancelamentoAgendamentoConsultaEspecialista")
            .Accepts<CancelAgendamentoByEspecialistaRequest>("application/json")
            .Produces<ApiResponse<int>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Cancelamento do agendamento de uma consulta pelo especialista")
            .WithSummary("Cancelamento do agendamento de uma consulta pelo especialista")
            .WithOpenApi();

        routes.MapPut("/confirmacao/paciente", static async (IMediator mediator, [FromBody] ConfirmAgendamentoByPacienteRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("ConfirmarAgendamentoConsultaPaciente")
            .Accepts<ConfirmAgendamentoByPacienteRequest>("application/json")
            .Produces<ApiResponse<int>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Confirmação do agendamento de uma consulta pelo paciente")
            .WithSummary("Confirmação do agendamento de uma consulta pelo paciente")
            .WithOpenApi();

        routes.MapPut("/confirmacao/especialista", static async (IMediator mediator, [FromBody] ConfirmAgendamentoByEspecialistaRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("ConfirmarAgendamentoConsultaEspecialista")
            .Accepts<ConfirmAgendamentoByEspecialistaRequest>("application/json")
            .Produces<ApiResponse<int>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Confirmação do agendamento de uma consulta pelo especialista")
            .WithSummary("Confirmação do agendamento de uma consulta pelo especialista")
            .WithOpenApi();
        #endregion

        #region [ GET ]
        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken)
            => await mediator.SendCommand(new SelectAgendamentoByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetAgendamentoById")
            .Produces<ApiResponse<AgendamentoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados do Agendamento da consulta pelo ID especificado")
            .WithSummary("Obter os dados do Agendamento da consulta pelo ID especificado")
            .WithOpenApi();        
        #endregion
        
        return routes;
    }
}