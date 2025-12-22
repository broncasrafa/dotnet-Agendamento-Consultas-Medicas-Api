using MediatR;

using Microsoft.AspNetCore.Mvc;

using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Query.GetAll;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Query.GetById;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Query.GetByName;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.Query.GetAll;
using RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.Query.GetById;
using RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByEspecialidadeTermPaged;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByFilterPaged;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class EspecialidadesEndpoints
{
    public static IEndpointRouteBuilder MapEspecialidadesEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/especialidades").WithTags("Especialidade").AllowAnonymous();

        routes.MapGet("/",
            static async (IMediator mediator, [FromQuery] string? name, CancellationToken cancellationToken) =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    return await mediator.SendCommand(new SelectEspecialidadeByNameRequest(name), cancellationToken: cancellationToken);
                }
                return await mediator.SendCommand(new SelectEspecialidadeRequest(), cancellationToken: cancellationToken);
            })
            .WithName("GetAllEspecialidades")
            .Produces<ApiListResponse<IReadOnlyList<EspecialidadeResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Especialidades ou buscar por nome")
            .WithSummary("Obter a lista de Especialidades ou buscar por nome")
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialidadeByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialidadesById")
            .Produces<ApiResponse<EspecialidadeResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a Especialidade pelo ID especificado")
            .WithSummary("Obter a Especialidade pelo ID especificado")
            .WithOpenApi();


        routes.MapGet("/grupos", static async (IMediator mediator, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialidadeGrupoRequest(), cancellationToken: cancellationToken))
            .WithName("GetAllGrupos")
            .Produces<ApiListResponse<IReadOnlyList<EspecialidadeGrupoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Grupos de Especialidades")
            .WithSummary("Obter a lista de Grupos de Especialidades")
            .WithOpenApi();

        routes.MapGet("/grupos/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectEspecialidadeGrupoByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneGrupoById")
            .Produces<ApiResponse<EspecialidadeGrupoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter o Grupo de Especialidade pelo ID especificado")
            .WithSummary("Obter o Grupo de Especialidade pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{term}/especialistas", static async (IMediator mediator, [FromRoute] string term, [FromQuery] int page = 1, [FromQuery] int items = 15, CancellationToken cancellationToken = default)
            => await mediator.SendCommand(new SelectEspecialistaByEspecialidadeTermRequest(term, items, page), cancellationToken: cancellationToken))
            .WithName("GetEspecialistasByEspecialidadeTermPaged")
            .Produces<PagedResult<EspecialistaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista paginada de Especialistas da Especialidade especificada")
            .WithSummary("Obter a lista paginada de Especialistas da Especialidade especificada")
            .AllowAnonymous()
            .WithOpenApi();

        return routes;
    }
}