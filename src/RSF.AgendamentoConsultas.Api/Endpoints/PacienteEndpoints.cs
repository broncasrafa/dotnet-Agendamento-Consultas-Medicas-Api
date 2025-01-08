using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.GetById;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.CreateDependentes;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.CreatePaciente;
using MediatR;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PacienteEndpoints
{
    public static IEndpointRouteBuilder MapPacienteEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/pacientes").WithTags("Pacientes");

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOnePacienteById")
            .Produces<ApiResponse<PacienteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados do Paciente pelo ID especificado")
            .WithSummary("Obter os dados do Paciente pelo ID especificado")
            .WithOpenApi();

        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreatePacienteRequest request, CancellationToken cancellationToken) => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreatePaciente")
            .Accepts<CreatePacienteRequest>("application/json")
            .Produces<ApiResponse<PacienteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar um Paciente")
            .WithSummary("Adicionar um Paciente ")
            .WithOpenApi();


        routes.MapPost("/{id:int}/dependentes", static async (IMediator mediator, [FromBody] CreatePacienteDependenteRequest request, [FromRoute] int id, CancellationToken cancellationToken) =>
        {
            if (id != request.PacientePrincipalId)
                throw new ValidationException("Os IDs do paciente principal não conferem");

            await mediator.SendCommand(request, cancellationToken: cancellationToken);
        })
            .WithName("CreateDependente")
            .Accepts<CreatePacienteDependenteRequest>("application/json")
            .Produces<ApiResponse<PacienteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar um Paciente Dependente para o Paciente principal pelo ID especificado do Paciente principal")
            .WithSummary("Adicionar um Paciente Dependente para o Paciente principal pelo ID especificado do Paciente principal")
            .WithOpenApi();


        return routes;
    }
}