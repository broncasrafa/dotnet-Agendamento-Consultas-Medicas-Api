using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Application.Features.Agendamento.Query.GetAgendamentoById;
using RSF.AgendamentoConsultas.Application.Features.Agendamento.Command;
using RSF.AgendamentoConsultas.Application.Features.Agendamento.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class AgendamentoConsultaEndpoints
{
    public static IEndpointRouteBuilder MapAgendamentoConsultaEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/agendamentos").WithTags("Agendamento");

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

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken)
            => await mediator.SendCommand(new SelectAgendamentoByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetAgendamentoById")
            .Produces<ApiResponse<AgendamentoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados do Agendamento da consulta pelo ID especificado")
            .WithSummary("Obter os dados do Agendamento da consulta pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}