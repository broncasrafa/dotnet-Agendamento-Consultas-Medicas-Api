﻿using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Avaliacao.Command.CreateAvaliacao;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Avaliacao.Query.GetAvaliacaoById;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Avaliacao.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class AvaliacaoEndpoints
{
    public static IEndpointRouteBuilder MapAvaliacaoEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/avaliacoes").WithTags("Avaliações");

        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreateAvaliacaoRequest request, CancellationToken cancellationToken) 
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreateAvaliacao")
            .Accepts<CreateAvaliacaoRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar uma avaliação sobre o Especialista")
            .WithSummary("Adicionar uma avaliação sobre o Especialista")
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectAvaliacaoByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetAvaliacaoById")
            .Produces<ApiResponse<AvaliacaoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados da Avaliação sobre o Especialista pelo ID especificado")
            .WithSummary("Obter os dados da Avaliação sobre o Especialista pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}