using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.GetAll;
using RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.GetById;
using RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class TipoStatusConsultaEndpoints
{
    public static IEndpointRouteBuilder MapTipoStatusConsultaEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/tipos/status-consulta")
                            .WithTags("Tipos de Status da Consulta")
                            .RequireAuthorization("OnlyAdmin");

        routes.MapGet("/", static async (IMediator mediator, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectTipoStatusConsultaRequest(), cancellationToken: cancellationToken))
            .WithName("GetAllTipoStatusConsulta")
            .Produces<ApiListResponse<IReadOnlyList<TipoStatusConsultaResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Obter a lista de Tipos de Consulta")
            .WithSummary("Obter a lista de Tipos de Consulta")
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectTipoStatusConsultaByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneTipoStatusConsultaById")
            .Produces<ApiResponse<TipoStatusConsultaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Obter o Tipo de Consulta pelo ID especificado")
            .WithSummary("Obter o Tipo de Consulta pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}