using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostasReacoes.Command.CreateReacao;
using RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostasReacoes.Command.UpdateReacao;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PerguntasRespostasReacoesEndpoints
{
    public static IEndpointRouteBuilder MapPerguntasRespostasReacoesEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/respostas/reacoes").WithTags("Respostas Reações");

        #region [ POST ]
        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreateReacaoRespostaRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreateReacaoResposta")
            .Accepts<CreateReacaoRespostaRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar uma Reação de like ou dislike a Resposta")
            .WithSummary("Adicionar uma Reação de like ou dislike a Resposta")
            .RequireAuthorization(policy => {
                policy.RequireRole(ETipoPerfilAcesso.Administrador.ToString());
                policy.RequireRole(ETipoPerfilAcesso.Paciente.ToString());
            })
            .WithOpenApi();
        #endregion

        #region [ PUT ]
        routes.MapPut("/", static async (IMediator mediator, [FromBody] UpdateReacaoRespostaRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("UpdateReacaoResposta")
            .Accepts<UpdateReacaoRespostaRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Atualiza os dados de uma Reação a Resposta, de like ou dislike ou retirar a reação")
            .WithSummary("Atualiza os dados de uma Reação a Resposta, de like ou dislike ou retirar a reação")
            .RequireAuthorization(policy => {
                policy.RequireRole(ETipoPerfilAcesso.Administrador.ToString());
                policy.RequireRole(ETipoPerfilAcesso.Paciente.ToString());
            })
            .WithOpenApi();
        #endregion

        return routes;
    }
}