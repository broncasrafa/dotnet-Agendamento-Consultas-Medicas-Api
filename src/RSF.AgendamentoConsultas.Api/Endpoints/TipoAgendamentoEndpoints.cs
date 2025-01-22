using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.GetAll;
using RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.GetById;
using RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class TipoAgendamentoEndpoints
{
    public static IEndpointRouteBuilder MapTipoAgendamentoEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/tipos/agendamento").WithTags("Tipos de Agendamento");

        routes.MapGet("/", static async (IMediator mediator, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectTipoAgendamentoRequest(), cancellationToken: cancellationToken))
            .WithName("GetAllTipoAgendamento")
            .Produces<ApiListResponse<IReadOnlyList<TipoAgendamentoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Tipos de Agendamento")
            .WithSummary("Obter a lista de Tipos de Agendamento")
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectTipoAgendamentoByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneTipoAgendamentoById")
            .Produces<ApiResponse<TipoAgendamentoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter o Tipo de Agendamento pelo ID especificado")
            .WithSummary("Obter o Tipo de Agendamento pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}