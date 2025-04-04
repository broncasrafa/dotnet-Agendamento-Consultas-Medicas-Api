﻿using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Core.Application.Features.Tags.Query.GetAll;
using RSF.AgendamentoConsultas.Core.Application.Features.Tags.Query.GetById;
using RSF.AgendamentoConsultas.Core.Application.Features.Tags.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class TagsEndpoints
{
    public static IEndpointRouteBuilder MapTagsEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/tags").WithTags("Tags");

        routes.MapGet("/", static async (IMediator mediator, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectTagsRequest(), cancellationToken: cancellationToken))
            .WithName("GetAllTags")
            .Produces<ApiListResponse<IReadOnlyList<TagsResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Obter a lista de Tags")
            .WithSummary("Obter a lista de Tags")
            .RequireAuthorization(ETipoRequireAuthorization.AdminOrPaciente.GetEnumDescription())
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectTagsByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneTagById")
            .Produces<ApiListResponse<TagsResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Obter a Tag pelo ID especificado")
            .WithSummary("Obter a Tag pelo ID especificado")
            .RequireAuthorization(ETipoRequireAuthorization.AdminOrPaciente.GetEnumDescription())
            .WithOpenApi();

        return routes;
    }
}