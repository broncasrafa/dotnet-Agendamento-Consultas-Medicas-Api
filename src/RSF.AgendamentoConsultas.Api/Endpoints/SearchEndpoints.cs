using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Core.Application.Features.Search.Query.GetByNameWithEspecialistas;
using RSF.AgendamentoConsultas.Core.Application.Features.Search.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class SearchEndpoints
{
    public static IEndpointRouteBuilder MapSearchesEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/search").WithTags("Search");

        routes.MapGet("/especialidades-and-especialistas",
            static async (IMediator mediator, [FromQuery] string? term, CancellationToken cancellationToken)
                => await mediator.SendCommand(new SelectEspecialidadesAndEspecialistasByNameRequest(term), cancellationToken: cancellationToken))
            .WithName("GetAllEspecialidadesAndEspecialistas")
            .Produces<ApiResponse<SearchEspecialidadesAndEspecialistasByNameResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obter a lista de Especialidades ou Especialistas pelo termo de busca especificado")
            .WithSummary("Obter a lista de Especialidades ou Especialistas pelo termo de busca especificado")
            .AllowAnonymous()
            .WithOpenApi();

        return routes;
    }
}