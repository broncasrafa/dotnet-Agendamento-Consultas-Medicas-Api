using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;
using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetRespostasByPerguntaId;
using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Command.CreateResposta;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PerguntasRespostasEndpoints
{
    public static IEndpointRouteBuilder MapPerguntasRespostasEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/respostas").WithTags("Respostas");

        routes.MapPost("/respostas", static async (IMediator mediator, [FromBody] CreateRespostaRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreateResposta")
            .Accepts<CreateRespostaRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar uma Resposta a pergunta feita ao Especialista pelo ID especificado da Pergunta")
            .WithSummary("Adicionar uma Resposta a pergunta feita ao Especialista pelo ID especificado da Pergunta")
            .WithOpenApi();


        routes.MapGet("/respostas/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken)
            => await mediator.SendCommand(new SelectRespostaByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetRespostaById")
            .Produces<ApiResponse<RespostaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados da Resposta a pergunta feita ao Especialista pelo ID especificado")
            .WithSummary("Obter os dados da Resposta a pergunta feita ao Especialista pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}