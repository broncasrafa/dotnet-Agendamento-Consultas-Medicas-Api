using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.GetAll;
using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.GetById;
using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.GetByIdWithCidadesAtendidas;
using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.GetByIdWithCidadesAtendidasPorEstado;
using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Responses;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class ConvenioMedicoEndpoints
{
    public static IEndpointRouteBuilder MapConvenioMedicoEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/convenios-medicos").WithTags("Convenio Medico");

        routes.MapGet("/", static async (IMediator mediator, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectConvenioMedicoRequest(), cancellationToken: cancellationToken))
            .WithName("GetAllConvenioMedicos")
            .Produces<ApiListResponse<IReadOnlyList<ConvenioMedicoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Convenios Medicos")
            .WithSummary("Obter a lista de Convenios Medicos")
            .WithOpenApi();

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectConvenioMedicoByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneConvenioMedico")
            .Produces<ApiListResponse<ConvenioMedicoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter o Convenio Medico pelo ID especificado")
            .WithSummary("Obter o Convenio Medico pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/cidades", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectConvenioMedicoByIdWithCidadesAtendidasRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneConvenioMedicoWithCidadesAtendidas")
            .Produces<ApiListResponse<ConvenioMedicoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter as cidades atendidas pelo Convenio Medico pelo ID especificado")
            .WithSummary("Obter as cidades atendidas pelo Convenio Medico pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/estado/{idEstado:int}/cidades", static async (IMediator mediator, [FromRoute] int id, [FromRoute] int idEstado, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequest(id, idEstado), cancellationToken: cancellationToken))
            .WithName("GetOneConvenioMedicoWithCidadesAtendidasPorEstado")
            .Produces<ApiListResponse<ConvenioMedicoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter as cidades atendidas pelo Convenio Medico pelo ID especificado do Convenio e do Estado")
            .WithSummary("Obter as cidades atendidas pelo Convenio Medico pelo ID especificado do Convenio e do Estado")
            .WithOpenApi();

        return routes;
    }
}