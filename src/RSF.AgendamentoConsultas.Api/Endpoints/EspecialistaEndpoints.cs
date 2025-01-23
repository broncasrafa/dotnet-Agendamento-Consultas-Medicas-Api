using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetById;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetEspecialistaRespostas;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithEspecialidades;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithConveniosMedicos;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithAvaliacoes;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithLocaisAtendimento;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithTags;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByNamePaged;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetAllPaged;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class EspecialistaEndpoints
{
    public static IEndpointRouteBuilder MapEspecialistaEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/especialistas").WithTags("Especialistas").AllowAnonymous();

        routes.MapGet("/", static async (IMediator mediator, [FromQuery] int page, [FromQuery] int items, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialistaPagedRequest(items, page), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaPaged")
            .Produces<PagedResult<EspecialistaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista paginada de Especialistas")
            .WithSummary("Obter a lista paginada de Especialistas")
            .WithOpenApi();

        routes.MapGet("/search", static async (IMediator mediator, [FromQuery] int page, [FromQuery] int items, [FromQuery] string name, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialistaByNamePagedRequest(name, items, page), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByNamePaged")
            .Produces<PagedResult<EspecialistaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista paginada de Especialistas pelo Nome especificado")
            .WithSummary("Obter a lista paginada de Especialistas pelo Nome especificado")
            .WithOpenApi();


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

        routes.MapGet("/{id:int}/respostas", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialistaRespostasRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdRespostas")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaPerguntaRespostaResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista das Respostas do Especialista pelo ID especificado")
            .WithSummary("Obter a lista das Respostas do Especialista pelo ID especificado")
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