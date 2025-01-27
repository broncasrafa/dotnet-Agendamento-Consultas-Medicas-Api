using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Query.GetRespostasById;
using RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Command.CreateResposta;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PerguntasRespostasEndpoints
{
    public static IEndpointRouteBuilder MapPerguntasRespostasEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/respostas").WithTags("Respostas");

        #region [ POST ]        
        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreateRespostaRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreateResposta")
            .Accepts<CreateRespostaRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar uma Resposta a pergunta feita ao Especialista pelo ID especificado da Pergunta")
            .WithSummary("Adicionar uma Resposta a pergunta feita ao Especialista pelo ID especificado da Pergunta")
            .RequireAuthorization("AdminOrEspecialista")
            .WithOpenApi();
        #endregion

        #region [ GET ]        
        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken)
            => await mediator.SendCommand(new SelectRespostaByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetRespostaById")
            .AllowAnonymous()
            .Produces<ApiResponse<RespostaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados da Resposta a pergunta feita ao Especialista pelo ID especificado")
            .WithSummary("Obter os dados da Resposta a pergunta feita ao Especialista pelo ID especificado")
            .WithOpenApi();
        #endregion

        return routes;
    }
}