using Microsoft.AspNetCore.Mvc;
using RSF.AgendamentoConsultas.Api.Extensions;
using RSF.AgendamentoConsultas.Api.Models;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
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
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddConvenioMedico;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddEspecialidade;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddLocalAtendimento;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteConvenioMedico;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteEspecialidade;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteLocalAtendimento;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateConvenioMedico;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateEspecialidade;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateLocalAtendimento;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateEspecialista;
using MediatR;

namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class EspecialistaEndpoints
{
    public static IEndpointRouteBuilder MapEspecialistaEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/especialistas").WithTags("Especialista");

        #region [ GET ]
        routes.MapGet("/", static async (IMediator mediator, [FromQuery] int page, [FromQuery] int items, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectEspecialistaPagedRequest(items, page), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaPaged")
            .Produces<PagedResult<EspecialistaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista paginada de Especialistas")
            .WithSummary("Obter a lista paginada de Especialistas")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapGet("/search", static async (IMediator mediator, [FromQuery] int page, [FromQuery] int items, [FromQuery] string name, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectEspecialistaByNamePagedRequest(name, items, page), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByNamePaged")
            .Produces<PagedResult<EspecialistaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista paginada de Especialistas pelo Nome especificado")
            .WithSummary("Obter a lista paginada de Especialistas pelo Nome especificado")
            .AllowAnonymous()
            .WithOpenApi();


        routes.MapGet("/{id:int}", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectEspecialistaByIdRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaById")
            .Produces<ApiResponse<EspecialistaResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter os dados do Especialista pelo ID especificado")
            .WithSummary("Obter os dados do Especialista pelo ID especificado")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapGet("/{id:int}/especialidades", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectEspecialistaByIdWithEspecialidadesRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdEspecialidades")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaEspecialidadeResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Especialidades do Especialista pelo ID especificado")
            .WithSummary("Obter a lista de Especialidades do Especialista pelo ID especificado")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapGet("/{id:int}/convenios-medicos", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken)
            => await mediator.SendCommand(new SelectEspecialistaByIdWithConveniosMedicosRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdConvenios")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaConvenioMedicoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Convênios Médicos atendidos pelo Especialista pelo ID especificado")
            .WithSummary("Obter a lista de Convênios Médicos atendidos pelo Especialista pelo ID especificado")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapGet("/{id:int}/avaliacoes", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectEspecialistaByIdWithAvaliacoesRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdAvaliacoes")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaAvaliacaoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista de Avaliações do Especialista pelo ID especificado")
            .WithSummary("Obter a lista de Avaliações do Especialista pelo ID especificado")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapGet("/{id:int}/locais-atendimento", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectEspecialistaByIdWithLocaisAtendimentoRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdLocaisAtendimento")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaLocalAtendimentoResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista dos Locais de Atendimento do Especialista pelo ID especificado")
            .WithSummary("Obter a lista dos Locais de Atendimento do Especialista pelo ID especificado")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapGet("/{id:int}/respostas", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectEspecialistaRespostasRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdRespostas")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaPerguntaRespostaResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista das Respostas do Especialista pelo ID especificado")
            .WithSummary("Obter a lista das Respostas do Especialista pelo ID especificado")
            .AllowAnonymous()
            .WithOpenApi();

        routes.MapGet("/{id:int}/marcacoes", static async (IMediator mediator, [FromRoute] int id, CancellationToken cancellationToken) 
            => await mediator.SendCommand(new SelectEspecialistaByIdWithTagsRequest(id), cancellationToken: cancellationToken))
            .WithName("GetOneEspecialistaByIdTags")
            .Produces<ApiResponse<EspecialistaResultList<EspecialistaTagsResponse>>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .WithDescription("Obter a lista das Marcações feitas pelos pacientes sobre o Especialista pelo ID especificado")
            .WithSummary("Obter a lista das Marcações feitas pelos pacientes sobre o Especialista pelo ID especificado")
            .AllowAnonymous()
            .WithOpenApi();
        #endregion

        #region [ POST ]
        routes.MapPost("/{id:int}/convenios-medicos", static async (IMediator mediator, [FromBody] AddConvenioMedicoRequest request, [FromRoute] int id, CancellationToken cancellationToken) 
            =>
            {
                if (id != request.EspecialistaId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do especialista não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
           .WithName("AddEspecialistaConvenioMedico")
           .Accepts<AddConvenioMedicoRequest>("application/json")
           .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
           .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
           .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
           .WithDescription("Adicionar o convênio médico atendido pelo Especialista pelo ID especificado do Especialista")
           .WithSummary("Adicionar o convênio médico atendido pelo Especialista pelo ID especificado do Especialista")
           .RequireAuthorization("AdminOrEspecialista")
           .WithOpenApi();

        routes.MapPost("/{id:int}/especialidades", static async (IMediator mediator, [FromBody] AddEspecialidadeRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.EspecialistaId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do especialista não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
           .WithName("AddEspecialistaEspecialidade")
           .Accepts<AddEspecialidadeRequest>("application/json")
           .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
           .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
           .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
           .WithDescription("Adicionar a especialidade atendida pelo Especialista pelo ID especificado do Especialista")
           .WithSummary("Adicionar a especialidade atendida pelo Especialista pelo ID especificado do Especialista")
           .RequireAuthorization("AdminOrEspecialista")
           .WithOpenApi();

        routes.MapPost("/{id:int}/locais-atendimento", static async (IMediator mediator, [FromBody] AddLocalAtendimentoRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
        {
            if (id != request.EspecialistaId)
                throw new InputRequestDataInvalidException("Id", "Os IDs do especialista não conferem");

            return await mediator.SendCommand(request, cancellationToken: cancellationToken);
        })
           .WithName("AddEspecialistaLocalAtendimento")
           .Accepts<AddLocalAtendimentoRequest>("application/json")
           .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
           .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
           .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
           .WithDescription("Adicionar o local de atendimento do Especialista pelo ID especificado do Especialista")
           .WithSummary("Adicionar o local de atendimento do Especialista pelo ID especificado do Especialista")
           .RequireAuthorization("AdminOrEspecialista")
           .WithOpenApi();
        #endregion

        #region [ PUT ]
        routes.MapPut("/{id:int}", static async (IMediator mediator, [FromBody] UpdateEspecialistaRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.EspecialistaId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do especialista não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
           .WithName("UpdateEspecialista")
           .Accepts<UpdateEspecialistaRequest>("application/json")
           .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
           .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
           .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
           .WithDescription("Atualizar os dados do Especialista pelo ID especificado do Especialista")
           .WithSummary("Atualizar os dados do Especialista pelo ID especificado do Especialista")
           .RequireAuthorization("AdminOrEspecialista")
           .WithOpenApi();

        routes.MapPut("/{id:int}/convenios-medicos", static async (IMediator mediator, [FromBody] UpdateConvenioMedicoRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.EspecialistaId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do especialista não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
           .WithName("UpdateEspecialistaConvenioMedico")
           .Accepts<UpdateConvenioMedicoRequest>("application/json")
           .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
           .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
           .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
           .WithDescription("Atualizar o convênio médico atendido pelo Especialista pelo ID especificado do Especialista")
           .WithSummary("Atualizar o convênio médico atendido pelo Especialista pelo ID especificado do Especialista")
           .RequireAuthorization("AdminOrEspecialista")
           .WithOpenApi();

        routes.MapPut("/{id:int}/especialidades", static async (IMediator mediator, [FromBody] UpdateEspecialidadeRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.EspecialistaId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do especialista não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
           .WithName("UpdateEspecialistaEspecialidade")
           .Accepts<UpdateEspecialidadeRequest>("application/json")
           .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
           .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
           .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
           .WithDescription("Atualizar a especialidade atendida pelo Especialista pelo ID especificado do Especialista")
           .WithSummary("Atualizar a especialidade atendida pelo Especialista pelo ID especificado do Especialista")
           .RequireAuthorization("AdminOrEspecialista")
           .WithOpenApi();

        routes.MapPut("/{id:int}/locais-atendimento", static async (IMediator mediator, [FromBody] UpdateLocalAtendimentoRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.EspecialistaId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do especialista não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
           .WithName("UpdateEspecialistaLocalAtendimento")
           .Accepts<UpdateLocalAtendimentoRequest>("application/json")
           .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
           .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
           .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
           .WithDescription("Atualizar o local de atendimento do Especialista pelo ID especificado do Especialista")
           .WithSummary("Atualizar o local de atendimento do Especialista pelo ID especificado do Especialista")
           .RequireAuthorization("AdminOrEspecialista")
           .WithOpenApi();
        #endregion

        #region [ DELETE ]
        routes.MapDelete("/{id:int}/convenios-medicos", static async (IMediator mediator, [FromBody] DeleteConvenioMedicoRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.EspecialistaId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do especialista não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
           .WithName("DeleteEspecialistaConvenioMedico")
           .Accepts<DeleteConvenioMedicoRequest>("application/json")
           .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
           .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
           .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
           .WithDescription("Deletar o convênio médico atendido pelo Especialista pelo ID especificado do Especialista")
           .WithSummary("Deletar o convênio médico atendido pelo Especialista pelo ID especificado do Especialista")
           .RequireAuthorization("AdminOrEspecialista")
           .WithOpenApi();

        routes.MapDelete("/{id:int}/especialidades", static async (IMediator mediator, [FromBody] DeleteEspecialidadeRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.EspecialistaId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do especialista não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
           .WithName("DeleteEspecialistaEspecialidade")
           .Accepts<DeleteEspecialidadeRequest>("application/json")
           .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
           .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
           .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
           .WithDescription("Deletar a especialidade atendida pelo Especialista pelo ID especificado do Especialista")
           .WithSummary("Deletar a especialidade atendida pelo Especialista pelo ID especificado do Especialista")
           .RequireAuthorization("AdminOrEspecialista")
           .WithOpenApi();

        routes.MapDelete("/{id:int}/locais-atendimento", static async (IMediator mediator, [FromBody] DeleteLocalAtendimentoRequest request, [FromRoute] int id, CancellationToken cancellationToken)
            =>
            {
                if (id != request.EspecialistaId)
                    throw new InputRequestDataInvalidException("Id", "Os IDs do especialista não conferem");

                return await mediator.SendCommand(request, cancellationToken: cancellationToken);
            })
           .WithName("DeleteEspecialistaLocalAtendimento")
           .Accepts<DeleteLocalAtendimentoRequest>("application/json")
           .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
           .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
           .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
           .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
           .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
           .WithDescription("Deletar o local de atendimento do Especialista pelo ID especificado do Especialista")
           .WithSummary("Deletar o local de atendimento do Especialista pelo ID especificado do Especialista")
           .RequireAuthorization("AdminOrEspecialista")
           .WithOpenApi();
        #endregion

        return routes;
    }
}