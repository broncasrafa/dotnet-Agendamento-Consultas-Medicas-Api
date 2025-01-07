using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetById;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithEspecialidades;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithConveniosMedicos;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithAvaliacoes;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithLocaisAtendimento;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithPerguntasRespostas;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetByIdWithTags;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class EspecialistaEndpoints
{
    public static IEndpointRouteBuilder MapEspecialistaEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/especialistas").WithTags("Especialistas");

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialistaByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaById")
            .Produces<ApiResponse<EspecialistaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados do Especialista pelo ID especificado")
            .WithSummary("Obter os dados do Especialista pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/especialidades", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialistaByIdWithEspecialidadesRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdEspecialidades")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaEspecialidadeResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Especialidades do Especialista pelo ID especificado")
            .WithSummary("Obter a lista de Especialidades do Especialista pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/convenios-medicos", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialistaByIdWithConveniosMedicosRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdConvenios")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaConvenioMedicoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Convênios Médicos atendidos pelo Especialista pelo ID especificado")
            .WithSummary("Obter a lista de Convênios Médicos atendidos pelo Especialista pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/avaliacoes", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialistaByIdWithAvaliacoesRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdAvaliacoes")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaAvaliacaoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Avaliações do Especialista pelo ID especificado")
            .WithSummary("Obter a lista de Avaliações do Especialista pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/locais-atendimento", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialistaByIdWithLocaisAtendimentoRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdLocaisAtendimento")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaLocalAtendimentoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista dos Locais de Atendimento do Especialista pelo ID especificado")
            .WithSummary("Obter a lista dos Locais de Atendimento do Especialista pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/perguntas-respostas", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialistaByIdWithPerguntasRespostasRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdPerguntasRespostas")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaPerguntaResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista das Perguntas feitas pelos pacientes e as Respostas do Especialista pelo ID especificado")
            .WithSummary("Obter a lista das Perguntas feitas pelos pacientes e as Respostas do Especialista pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/marcacoes", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialistaByIdWithTagsRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdTags")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaTagsResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista das Marcações feitas pelos pacientes sobre o Especialista pelo ID especificado")
            .WithSummary("Obter a lista das Marcações feitas pelos pacientes sobre o Especialista pelo ID especificado")
            .WithOpenApi();

        return routes;
    }
}