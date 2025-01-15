using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Application.Features.Pergunta.Command.CreatePergunta;
using RSF.AgendamentoConsultas.Application.Features.Pergunta.Query.GetPerguntaById;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PerguntasEndpoints
{
    public static IEndpointRouteBuilder MapPerguntasEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/perguntas").WithTags("Perguntas");

        routes.MapPost("/perguntas", static async (IMediator mediator, [FromBody] CreatePerguntaRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreatePergunta")
            .Accepts<CreatePerguntaRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar uma pergunta para uma Especialidade")
            .WithSummary("Adicionar uma pergunta para uma Especialidade")
            .WithOpenApi();


        routes.MapGet("/perguntas/{id:int}/{especialidadeId:int}", static async (IMediator mediator, [FromRoute] int id, [FromRoute] int especialidadeId, CancellationToken cancellationToken)
            => await mediator.SendCommand(new SelectPerguntaByIdRequest(id, especialidadeId), cancellationToken: cancellationToken))
            .WithName("GetPerguntaById")
            .Produces<ApiResponse<PerguntaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados da Pergunta pelo ID especificado da Pergunta e da Especialidade")
            .WithSummary("Obter os dados da Pergunta pelo ID especificado da Pergunta e da Especialidade")
            .WithOpenApi();

        return routes;
    }
}