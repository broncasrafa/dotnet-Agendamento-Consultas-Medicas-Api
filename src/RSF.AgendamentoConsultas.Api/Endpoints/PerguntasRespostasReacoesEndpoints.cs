using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostasReacoes.Command.CreateReacao;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PerguntasRespostasReacoesEndpoints
{
    public static IEndpointRouteBuilder MapPerguntasRespostasReacoesEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/reacoes-respostas").WithTags("Reações Respostas");

        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreateReacaoRespostaRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreateReacaoResposta")
            .Accepts<CreateReacaoRespostaRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar uma Reação de like ou dislike a Resposta")
            .WithSummary("Adicionar uma Reação de like ou dislike a Resposta")
            .WithOpenApi();

        #region [ POST ]
        #endregion

        #region [ PUT ]
        #endregion

        #region [ DELETE ]
        #endregion

        return routes;
    }
}