using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.GetAll;
using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.GetById;
using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.SearchByName;
using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class CidadeEndpoints
{
    public static IEndpointRouteBuilder MapCidadeEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/common/cidades").WithTags("Common").AllowAnonymous();

        routes.MapGet("/", static async (IMediator mediator, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectCidadesRequest(), cancellationToken: cancellationToken))
            .WithName("GetAllCidades")
            .Produces<ApiListResponse<CidadeResponse>>(StatusCodes.Status200OK)
            .WithDescription("Obter a lista de cidades")
            .WithSummary("Obter a lista de cidades")
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectCidadeByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneCidade")
            .Produces<ApiListResponse<CidadeResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a cidade pelo ID especificado")
            .WithSummary("Obter a cidade pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/search", static async (IMediator mediator, [FromQuery] string name, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectCidadesByNameRequest(name), cancellationToken: cancellationToken))
            .WithName("SearchCidadesByName")
            .Produces<ApiListResponse<CidadeResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de cidades pelo nome especificado na busca")
            .WithSummary("Obter a lista de cidades pelo nome especificado na busca")
            .WithOpenApi();

        return routes;
    }
}