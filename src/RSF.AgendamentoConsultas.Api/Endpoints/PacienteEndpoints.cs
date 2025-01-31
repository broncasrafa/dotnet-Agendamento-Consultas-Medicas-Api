using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteById;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdDependentes;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdPlanosMedicos;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAgendamentos;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAvaliacoes;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.CreatePacientePlanoMedico;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.UpdatePaciente;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.UpdatePacientePlanoMedico;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePaciente;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePacientePlanoMedico;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePacienteAgendamento;
using MediatR;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class PacienteEndpoints
{
    public static IEndpointRouteBuilder MapPacienteEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/pacientes")
                            .WithTags("Paciente")
                            .RequireAuthorization("AdminOrPaciente");

        #region [ GET ]

        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOnePacienteById")
            .Produces<ApiResponse<PacienteResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Obter os dados do Paciente pelo ID especificado")
            .WithSummary("Obter os dados do Paciente pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/dependentes", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteDependentesRequest(id), cancellationToken: cancellationToken))
            .WithName("GetDependentesForOnePacienteById")
            .Produces<ApiResponse<PacienteResultList<PacienteDependenteResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Obter a lista de Dependentes do Paciente pelo ID especificado")
            .WithSummary("Obter a lista de Dependentes do Paciente pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/planos-medicos", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacientePlanosMedicosRequest(id), cancellationToken: cancellationToken))
            .WithName("GetPlanosMedicosForOnePacienteById")
            .Produces<ApiResponse<PacienteResultList<PacientePlanoMedicoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Obter a lista dos Planos Medicos do Paciente pelo ID especificado")
            .WithSummary("Obter a lista dos Planos Medicos do Paciente pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/agendamentos", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteAgendamentosRequest(id), cancellationToken: cancellationToken))
            .WithName("GetAgendamentosForOnePacienteById")
            .Produces<ApiResponse<PacienteResultList<AgendamentoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Obter a lista dos Agendamentos realizados do Paciente pelo ID especificado")
            .WithSummary("Obter a lista dos Agendamentos realizados do Paciente pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/avaliacoes", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) => await mediator.SendCommand(new SelectPacienteAvaliacoesRequest(id), cancellationToken: cancellationToken))
            .WithName("GetAvaliacoesForOnePacienteById")
            .Produces<ApiResponse<PacienteResultList<PacienteAvaliacaoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Obter a lista dos Avaliações feitas pelo Paciente pelo ID especificado")
            .WithSummary("Obter a lista dos Avaliações feitas pelo Paciente pelo ID especificado")
            .WithOpenApi();

        #endregion

        #region [ POST ]

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
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Adicionar um Plano Medico para o Paciente pelo ID especificado do Paciente")
            .WithSummary("Adicionar um Plano Medico para o Paciente pelo ID especificado do Paciente")
            .WithOpenApi();

        #endregion

        #region [ PUT ]
        routes.MapPut("/{id:int}", static async (IMediator mediator, [FromBody] UpdatePacienteRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.PacienteId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("UpdatePaciente")
            .Accepts<UpdatePacienteRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Atualizar os dados de um Paciente pelo ID especificado")
            .WithSummary("Atualizar os dados de um Paciente pelo ID especificado")
            .WithOpenApi();

        routes.MapPut("/{id:int}/planos-medicos", static async (IMediator mediator, [FromBody] UpdatePacientePlanoMedicoRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.PacienteId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("UpdatePacientePlanoMedico")
            .Accepts<UpdatePacientePlanoMedicoRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Atualizar os dados do Plano Medico do Paciente pelo ID especificado")
            .WithSummary("Atualizar os dados do Plano Medico do Paciente pelo ID especificado")
            .WithOpenApi();
        #endregion

        #region [ DELETE ]
        routes.MapDelete("/{id:int}", static async (IMediator mediator, [FromBody] DeletePacienteRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.PacienteId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("DeletePaciente")
            .Accepts<DeletePacienteRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Deleta os dados de um Paciente pelo ID especificado")
            .WithSummary("Deleta os dados de um Paciente pelo ID especificado")
            .WithOpenApi();

        routes.MapDelete("/{id:int}/planos-medicos", static async (IMediator mediator, [FromBody] DeletePacientePlanoMedicoRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.PacienteId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("DeletePacientePlanoMedico")
            .Accepts<DeletePacientePlanoMedicoRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Deleta os dados do Plano Medico do Paciente pelo ID especificado")
            .WithSummary("Deleta os dados do Plano Medico do Paciente pelo ID especificado")
            .WithOpenApi();

        routes.MapDelete("/{id:int}/agendamentos", static async (IMediator mediator, [FromBody] DeletePacienteAgendamentoRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.PacienteId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do paciente não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
            .WithName("DeletePacienteAgendamento")
            .Accepts<DeletePacienteAgendamentoRequest>("application/json")
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .WithDescription("Cancela um agendamento do Paciente pelo ID especificado")
            .WithSummary("Cancela um agendamento Paciente pelo ID especificado")
            .WithOpenApi();
        #endregion

        return routes;
    }
}