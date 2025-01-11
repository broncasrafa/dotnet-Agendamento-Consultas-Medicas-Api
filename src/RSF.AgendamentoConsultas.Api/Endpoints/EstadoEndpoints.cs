using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Application.Features.Estado.GetAll;
using RSF.AgendamentoConsultas.Application.Features.Estado.GetById;
using RSF.AgendamentoConsultas.Application.Features.Estado.GetByIdWithCidades;
using RSF.AgendamentoConsultas.Application.Features.Estado.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class EstadoEndpoints
{
    public static IEndpointRouteBuilder MapEstadoEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/common/estados").WithTags("Commons");

        routes.MapGet("/", static async (IMediator mediator, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEstadoRequest(), cancellationToken: cancellationToken))
            .WithName("GetAllEstados")
            .Produces<ApiListResponse<IReadOnlyList<EstadoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de estados do país")
            .WithSummary("Obter a lista de estados do país")
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEstadoByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEstado")
            .Produces<ApiListResponse<EstadoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter o estado do país pelo ID especificado")
            .WithSummary("Obter o estado do país pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/cidades", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEstadoByIdWithCidadesRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEstadoWithCidades")
            .Produces<ApiListResponse<EstadoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter as cidades da estado do país pelo ID especificado")
            .WithSummary("Obter as cidades da estado do país pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}