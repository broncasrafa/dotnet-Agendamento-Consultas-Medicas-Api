using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.GetAll;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.GetById;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class TagsEndpoints
{
    public static IEndpointRouteBuilder MapTagsEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/tags").WithTags("Tags");

        routes.MapGet("/", static async (IMediator mediator, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectTagsRequest(), cancellationToken: cancellationToken))
            .WithName("GetAll")
            .Produces<ApiListResponse<IReadOnlyList<TagsResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Tags")
            .WithSummary("Obter a lista de Tags")
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectTagsByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneById")
            .Produces<ApiListResponse<TagsResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a Tag pelo ID especificado")
            .WithSummary("Obter a Tag pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}