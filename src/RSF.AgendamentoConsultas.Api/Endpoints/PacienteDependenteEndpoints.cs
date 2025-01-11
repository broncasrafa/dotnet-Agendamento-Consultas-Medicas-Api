using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Query.GetDependenteById;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.CreateDependente;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.CreateDependentePlanoMedico;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.DeleteDependente;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.DeleteDependentePlanoMedico;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.UpdateDependente;
using RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Command.UpdateDependentePlanoMedico;
using MediatR;
using FluentValidation;


namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PacienteDependenteEndpoints
{
    public static IEndpointRouteBuilder MapPacienteDependenteEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/pacientes/{idPacientePrincipal:int}/dependentes").WithTags("Pacientes Dependentes");

        #region [ POST ]
        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreatePacienteDependenteRequest request, [FromRoute] int idPacientePrincipal, CancellationToken cancellationToken) 
            =>
            {
                if (idPacientePrincipal != request.PacientePrincipalId)
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


        routes.MapPost("/{idDependente:int}/planos-medicos", static async (IMediator mediator, [FromBody] CreatePacienteDependentePlanoMedicoRequest request, [FromRoute] int idPacientePrincipal, [FromRoute] int idDependente, CancellationToken cancellationToken) 
            =>
            {
                if (idPacientePrincipal != request.PacientePrincipalId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente principal não conferem");

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
        routes.MapGet("/{idDependente:int}", static async (IMediator mediator, [FromRoute] int idPacientePrincipal, [FromRoute] int idDependente, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteDependenteByIdRequest(idPacientePrincipal, idDependente), cancellationToken: cancellationToken))
            .WithName("GetOneDependenteById")
            .Produces<ApiResponse<PacienteDependenteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados do Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithSummary("Obter os dados do Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithOpenApi();


        routes.MapGet("/{idDependente:int}/planos-medicos", static async (IMediator mediator, [FromRoute] int idPacientePrincipal, [FromRoute] int idDependente, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteDependentePlanosMedicosRequest(idPacientePrincipal, idDependente), cancellationToken: cancellationToken))
            .WithName("GetPlanosMedicosForOneDependenteById")
            .Produces<ApiResponse<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista dos Planos Medicos do Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithSummary("Obter a lista dos Planos Medicos do Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithOpenApi();

        #endregion

        #region [ PUT ]
        routes.MapPut("/", static async (IMediator mediator, [FromBody] UpdatePacienteDependenteRequest request, [FromRoute] int idPacientePrincipal, CancellationToken cancellationToken) 
            =>
                {
                    if (idPacientePrincipal != request.PacientePrincipalId)
                        throw new InputRequestDataInvalidException("Id", "Os IDs do paciente principal não conferem");

                    return await mediator.SendCommand(request, cancellationToken: cancellationToken);
                })
            .WithName("UpdateDependente")
            .Accepts<UpdatePacienteDependenteRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Atualizar os dados de um Paciente Dependente pelo ID especificado do Paciente principal")
            .WithSummary("Atualizar os dados de um Paciente Dependente pelo ID especificado do Paciente principal")
            .WithOpenApi();


        routes.MapPut("/{idDependente:int}/planos-medicos", static async (IMediator mediator, [FromBody] UpdatePacienteDependentePlanoMedicoRequest request, [FromRoute] int idPacientePrincipal, [FromRoute] int idDependente, CancellationToken cancellationToken) 
            =>
            {
                if (idPacientePrincipal != request.PacientePrincipalId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente principal não conferem");

                if (idDependente != request.DependenteId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente dependente não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("UpdateDependentePlanoMedico")
            .Accepts<UpdatePacienteDependentePlanoMedicoRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Atualizar os dados do Plano Medico para o Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithSummary("Atualizar os dados do Plano Medico para o Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithOpenApi();

        #endregion

        #region [ DELETE ]
        routes.MapDelete("/", static async (IMediator mediator, [FromBody] DeletePacienteDependenteRequest request, [FromRoute] int idPacientePrincipal, CancellationToken cancellationToken)
            =>
            {
                if (idPacientePrincipal != request.PacientePrincipalId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente principal não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("DeleteDependente")
            .Accepts<DeletePacienteDependenteRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Deleta os dados de um Paciente Dependente pelo ID especificado do Paciente principal")
            .WithSummary("Deleta os dados de um Paciente Dependente pelo ID especificado do Paciente principal")
            .WithOpenApi();


        routes.MapDelete("/{idDependente:int}/planos-medicos", static async (IMediator mediator, [FromBody] DeletePacienteDependentePlanoMedicoRequest request, [FromRoute] int idPacientePrincipal, [FromRoute] int idDependente, CancellationToken cancellationToken)
            =>
            {
                if (idPacientePrincipal != request.PacientePrincipalId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente principal não conferem");

                if (idDependente != request.DependenteId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente dependente não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("DeleteDependentePlanoMedico")
            .Accepts<DeletePacienteDependentePlanoMedicoRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Deleta os dados do Plano Medico para o Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithSummary("Deleta os dados do Plano Medico para o Paciente Dependente pelo ID especificado do Paciente Dependente e do Paciente principal")
            .WithOpenApi();
        #endregion

        return routes;
    }
}