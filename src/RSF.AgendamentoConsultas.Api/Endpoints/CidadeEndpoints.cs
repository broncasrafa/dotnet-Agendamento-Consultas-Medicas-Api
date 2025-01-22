using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.GetById;
using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class CidadeEndpoints
{
    public static IEndpointRouteBuilder MapCidadeEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/common/cidades").WithTags("Commons");

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectCidadeByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneCidade")
            .Produces<ApiListResponse<CidadeResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a cidade pelo ID especificado")
            .WithSummary("Obter a cidade pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}