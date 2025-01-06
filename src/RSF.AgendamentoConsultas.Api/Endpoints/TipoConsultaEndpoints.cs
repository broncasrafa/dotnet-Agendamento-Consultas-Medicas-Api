using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.GetAll;
using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.GetById;
using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class TipoConsultaEndpoints
{
    public static IEndpointRouteBuilder MapTipoConsultaEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/tipos/consulta").WithTags("Tipos de Consulta");

        routes.MapGet("/", static async (IMediator mediator, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectTipoConsultaRequest(), cancellationToken: cancellationToken))
            .WithName("GetAllTipoConsulta")
            .Produces<ApiListResponse<IReadOnlyList<TipoConsultaResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Tipos de Consulta")
            .WithSummary("Obter a lista de Tipos de Consulta")
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectTipoConsultaByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneTipoConsultaById")
            .Produces<ApiResponse<TipoConsultaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter o Tipo de Consulta pelo ID especificado")
            .WithSummary("Obter o Tipo de Consulta pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}