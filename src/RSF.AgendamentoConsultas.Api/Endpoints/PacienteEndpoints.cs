using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Query.GetPacienteById;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Query.GetPacienteByIdDependentes;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Query.GetPacienteByIdPlanosMedicos;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Command.CreatePaciente;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Command.CreatePacientePlanoMedico;
using MediatR;
using FluentValidation;


namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PacienteEndpoints
{
    public static IEndpointRouteBuilder MapPacienteEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/pacientes").WithTags("Pacientes");

        #region [ GET ]

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOnePacienteById")
            .Produces<ApiResponse<PacienteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados do Paciente pelo ID especificado")
            .WithSummary("Obter os dados do Paciente pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/dependentes", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteDependentesRequest(id), cancellationToken: cancellationToken))
            .WithName("GetDependentesForOnePacienteById")
            .Produces<ApiResponse<PacienteResultList<PacienteDependenteResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Dependentes do Paciente pelo ID especificado")
            .WithSummary("Obter a lista de Dependentes do Paciente pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/planos-medicos", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacientePlanosMedicosRequest(id), cancellationToken: cancellationToken))
            .WithName("GetPlanosMedicosForOnePacienteById")
            .Produces<ApiResponse<PacienteResultList<PacientePlanoMedicoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista dos Planos Medicos do Paciente pelo ID especificado")
            .WithSummary("Obter a lista dos Planos Medicos do Paciente pelo ID especificado")
            .WithOpenApi();

        #endregion

        #region [ POST ]

        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreatePacienteRequest request, CancellationToken cancellationToken) => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreatePaciente")
            .Accepts<CreatePacienteRequest>("application/json")
            .Produces<ApiResponse<PacienteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar um Paciente")
            .WithSummary("Adicionar um Paciente ")
            .WithOpenApi();


        routes.MapPost("/{id:int}/planos-medicos", static async (IMediator mediator, [FromBody] CreatePacientePlanoMedicoRequest request, [FromRoute] int id, CancellationToken cancellationToken) =>
        {
            if (id != request.PacienteId)
                throw new InputRequestDataInvalidException("Id", "Os IDs do paciente não conferem");

            return await mediator.SendCommand(request, cancellationToken: cancellationToken);
        })
            .WithName("CreatePacientePlanoMedico")
            .Accepts<CreatePacientePlanoMedicoRequest>("application/json")
            .Produces<ApiResponse<PacientePlanoMedicoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar um Plano Medico para o Paciente pelo ID especificado do Paciente")
            .WithSummary("Adicionar um Plano Medico para o Paciente pelo ID especificado do Paciente")
            .WithOpenApi();

        #endregion

        return routes;
    }
}