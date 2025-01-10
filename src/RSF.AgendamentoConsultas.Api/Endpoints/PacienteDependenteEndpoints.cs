using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Command.CreateDependente;
using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Command.CreateDependentePlanoMedico;
using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Query.GetDependenteById;
using RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;
using MediatR;
using FluentValidation;
using RSF.AgendamentoConsultas.Shareable.Exceptions;


namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PacienteDependenteEndpoints
{
    public static IEndpointRouteBuilder MapPacienteDependenteEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/pacientes/{id:int}/dependentes").WithTags("Pacientes Dependentes");

        #region [ POST ]
        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreatePacienteDependenteRequest request, [FromRoute] int id, CancellationToken cancellationToken) =>
        {
            if (id != request.PacientePrincipalId)
                throw new InputRequestDataInvalidException("Id", "Os IDs do paciente principal não conferem");

            return await mediator.SendCommand(request, cancellationToken: cancellationToken);
        })
            .WithName("CreateDependente")
            .Accepts<CreatePacienteDependenteRequest>("application/json")
            .Produces<ApiResponse<PacienteDependenteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar um Paciente Dependente para o Paciente principal pelo ID especificado do Paciente principal")
            .WithSummary("Adicionar um Paciente Dependente para o Paciente principal pelo ID especificado do Paciente principal")
            .WithOpenApi();


        routes.MapPost("/{idDependente:int}/planos-medicos", static async (IMediator mediator, [FromBody] CreatePacienteDependentePlanoMedicoRequest request, [FromRoute] int id, [FromRoute] int idDependente, CancellationToken cancellationToken) =>
        {
            if (idDependente != request.DependenteId)
                throw new InputRequestDataInvalidException("Id", "Os IDs do paciente dependente não conferem");

            return await mediator.SendCommand(request, cancellationToken: cancellationToken);
        })
            .WithName("CreatePacienteDependentePlanoMedico")
            .Accepts<CreatePacienteDependentePlanoMedicoRequest>("application/json")
            .Produces<ApiResponse<PacienteDependentePlanoMedicoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar um Plano Medico para o Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithSummary("Adicionar um Plano Medico para o Paciente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithOpenApi();

        #endregion

        #region [ GET] 
        routes.MapGet("/{idDependente:int}", static async (IMediator mediator, [FromRoute] int id, [FromRoute] int idDependente, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteDependenteByIdRequest(id, idDependente), cancellationToken: cancellationToken))
            .WithName("GetOneDependenteById")
            .Produces<ApiResponse<PacienteDependenteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados do Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithSummary("Obter os dados do Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithOpenApi();


        routes.MapGet("/{idDependente:int}/planos-medicos", static async (IMediator mediator, [FromRoute] int id, [FromRoute] int idDependente, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteDependentePlanosMedicosRequest(idDependente, id), cancellationToken: cancellationToken))
            .WithName("GetPlanosMedicosForOneDependenteById")
            .Produces<ApiResponse<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista dos Planos Medicos do Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithSummary("Obter a lista dos Planos Medicos do Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithOpenApi();

        #endregion

        return routes;
    }
}