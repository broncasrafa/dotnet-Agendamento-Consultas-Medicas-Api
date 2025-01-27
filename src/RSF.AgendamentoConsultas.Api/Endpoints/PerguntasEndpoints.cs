using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Command.CreatePergunta;
using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaById;
using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaByIdAndIdEspecialidade;
using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaByIdRespostas;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PerguntasEndpoints
{
    public static IEndpointRouteBuilder MapPerguntasEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/perguntas").WithTags("Perguntas");

        #region [ POST ]
        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreatePerguntaRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreatePergunta")
            .Accepts<CreatePerguntaRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Adicionar uma pergunta para uma Especialidade")
            .WithSummary("Adicionar uma pergunta para uma Especialidade")
            .RequireAuthorization("AdminOrPaciente")
            .WithOpenApi();
        #endregion

        #region [ GET ]
        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken)
            => await mediator.SendCommand(new SelectPerguntaByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetPerguntaById")
            .AllowAnonymous()
            .Produces<ApiResponse<PerguntaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados da Pergunta pelo ID especificado")
            .WithSummary("Obter os dados da Pergunta pelo ID especificado")
            .WithOpenApi();


        routes.MapGet("/{id:int}/respostas", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken)
            => await mediator.SendCommand(new SelectPerguntaByIdRespostasRequest(id), cancellationToken: cancellationToken))
            .WithName("GetPerguntaByIdRespostas")
            .AllowAnonymous()
            .Produces<ApiResponse<PerguntaResultList<RespostaResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados da Pergunta pelo ID especificado")
            .WithSummary("Obter os dados da Pergunta pelo ID especificado")
            .WithOpenApi();


        routes.MapGet("/{id:int}/{especialidadeId:int}", static async (IMediator mediator, [FromRoute] int id, [FromRoute] int especialidadeId, CancellationToken cancellationToken)
            => await mediator.SendCommand(new SelectPerguntaByIdAndIdEspecialidadeRequest(id, especialidadeId), cancellationToken: cancellationToken))
            .WithName("GetPerguntaByIdAndIdEspecialidade")
            .Produces<ApiResponse<PerguntaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Obter os dados da Pergunta pelo ID especificado da Pergunta e da Especialidade")
            .WithSummary("Obter os dados da Pergunta pelo ID especificado da Pergunta e da Especialidade")
            .RequireAuthorization("AdminOrEspecialista")
            .WithOpenApi();
        #endregion

        return routes;
    }
}

