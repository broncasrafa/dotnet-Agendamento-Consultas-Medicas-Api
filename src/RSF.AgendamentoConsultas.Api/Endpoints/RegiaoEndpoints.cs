using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetAll;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetById;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetByIdWithEstados;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class RegiaoEndpoints
{
    public static IEndpointRouteBuilder MapRegiaoEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/common/regioes").WithTags("Commons");

        routes.MapGet("/", static async (IMediator mediator, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectRegiaoRequest(), cancellationToken: cancellationToken))
            .WithName("GetAllRegioes")
            .Produces<ApiListResponse<IReadOnlyList<RegiaoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de regiões do país")
            .WithSummary("Obter a lista de regiões do país")
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectRegiaoByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneRegiao")
            .Produces<ApiListResponse<RegiaoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a região do país pelo ID especificado")
            .WithSummary("Obter a região do país pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/estados", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectRegiaoByIdWithEstadosRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneRegiaoWithEstados")
            .Produces<ApiListResponse<RegiaoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os estados da região do país pelo ID especificado")
            .WithSummary("Obter os estados da região do país pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}