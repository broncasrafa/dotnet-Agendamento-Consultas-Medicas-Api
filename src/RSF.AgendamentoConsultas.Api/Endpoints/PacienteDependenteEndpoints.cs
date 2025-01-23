using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteById;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Query.GetDependenteByIdPlanosMedicos;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.CreateDependente;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.CreateDependentePlanoMedico;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.DeleteDependente;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.DeleteDependentePlanoMedico;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.UpdateDependente;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Command.UpdateDependentePlanoMedico;
using MediatR;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PacienteDependenteEndpoints
{
    public static IEndpointRouteBuilder MapPacienteDependenteEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/dependentes")
                            .WithTags("Pacientes Dependentes")
                            .RequireAuthorization(policy => {
                                policy.RequireRole(ETipoPerfilAcesso.Administrador.ToString());
                                policy.RequireRole(ETipoPerfilAcesso.Paciente.ToString());
                            });

        #region [ POST ]
        routes.MapPost("/", static async (IMediator mediator, [FromBody] CreatePacienteDependenteRequest request, CancellationToken cancellationToken) 
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("CreateDependente")
            .Accepts<CreatePacienteDependenteRequest>("application/json")
            .Produces<ApiResponse<PacienteDependenteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar um Paciente Dependente para o Paciente principal")
            .WithSummary("Adicionar um Paciente Dependente para o Paciente principal")
            .WithOpenApi();


        routes.MapPost("/{id:int}/planos-medicos", static async (IMediator mediator, [FromBody] CreatePacienteDependentePlanoMedicoRequest request, [FromRoute] int id, CancellationToken cancellationToken) 
            =>
            {
                if (id != request.DependenteId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente dependente não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("CreatePacienteDependentePlanoMedico")
            .Accepts<CreatePacienteDependentePlanoMedicoRequest>("application/json")
            .Produces<ApiResponse<PacienteDependentePlanoMedicoResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Adicionar um Plano Medico para o Paciente Dependente pelo ID especificado do Paciente Dependente")
            .WithSummary("Adicionar um Plano Medico para o Paciente Dependente pelo ID especificado do Paciente Dependente")
            .WithOpenApi();

        #endregion

        #region [ GET] 
        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectPacienteDependenteByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneDependenteById")
            .Produces<ApiResponse<PacienteDependenteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados do Paciente Dependente pelo ID especificado do Paciente Dependente")
            .WithSummary("Obter os dados do Paciente Dependente pelo ID especificado do Paciente Dependente")
            .WithOpenApi();


        routes.MapGet("/{id:int}/planos-medicos", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectPacienteDependentePlanosMedicosRequest(id), cancellationToken: cancellationToken))
            .WithName("GetPlanosMedicosForOneDependenteById")
            .Produces<ApiResponse<PacienteDependenteResultList<PacienteDependentePlanoMedicoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista dos Planos Medicos do Paciente Dependente pelo ID especificado do Paciente Dependente")
            .WithSummary("Obter a lista dos Planos Medicos do Paciente Dependente pelo ID especificado do Paciente Dependente")
            .WithOpenApi();

        #endregion

        #region [ PUT ]
        routes.MapPut("/", static async (IMediator mediator, [FromBody] UpdatePacienteDependenteRequest request, CancellationToken cancellationToken) 
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("UpdateDependente")
            .Accepts<UpdatePacienteDependenteRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Atualizar os dados de um Paciente Dependente")
            .WithSummary("Atualizar os dados de um Paciente Dependente")
            .WithOpenApi();


        routes.MapPut("/{id:int}/planos-medicos", static async (IMediator mediator, [FromBody] UpdatePacienteDependentePlanoMedicoRequest request, [FromRoute] int id, CancellationToken cancellationToken) 
            =>
            {
                if (id != request.DependenteId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente dependente não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("UpdateDependentePlanoMedico")
            .Accepts<UpdatePacienteDependentePlanoMedicoRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Atualizar os dados do Plano Medico do Paciente Dependente pelo ID especificado do Paciente Dependente")
            .WithSummary("Atualizar os dados do Plano Medico do Paciente Dependente pelo ID especificado do Paciente Dependente")
            .WithOpenApi();

        #endregion

        #region [ DELETE ]
        routes.MapDelete("/", static async (IMediator mediator, [FromBody] DeletePacienteDependenteRequest request, CancellationToken cancellationToken)
            => await mediator.SendCommand(request, cancellationToken: cancellationToken))
            .WithName("DeleteDependente")
            .Accepts<DeletePacienteDependenteRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Deleta os dados de um Paciente Dependente")
            .WithSummary("Deleta os dados de um Paciente Dependente")
            .WithOpenApi();


        routes.MapDelete("/{id:int}/planos-medicos", static async (IMediator mediator, [FromBody] DeletePacienteDependentePlanoMedicoRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.DependenteId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente dependente não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("DeleteDependentePlanoMedico")
            .Accepts<DeletePacienteDependentePlanoMedicoRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Deleta os dados do Plano Medico do Paciente Dependente pelo ID especificado do Paciente Dependente")
            .WithSummary("Deleta os dados do Plano Medico do Paciente Dependente pelo ID especificado do Paciente Dependente")
            .WithOpenApi();
        #endregion

        return routes;
    }
}