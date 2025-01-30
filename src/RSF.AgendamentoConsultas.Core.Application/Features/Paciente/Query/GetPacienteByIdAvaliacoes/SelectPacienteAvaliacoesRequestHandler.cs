using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using MediatR;
using OperationResult;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Query.GetPacienteByIdAvaliacoes;

public class SelectPacienteAvaliacoesRequestHandler : IRequestHandler<SelectPacienteAvaliacoesRequest, Result<PacienteResultList<PacienteAvaliacaoResponse>>>
{
    private readonly IPacienteRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public SelectPacienteAvaliacoesRequestHandler(IPacienteRepository pacienteRepository, IHttpContextAccessor httpContext)
    {
        _repository = pacienteRepository;
        _httpContext = httpContext;
    }

    public async Task<Result<PacienteResultList<PacienteAvaliacaoResponse>>> Handle(SelectPacienteAvaliacoesRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacienteId, ETipoPerfilAcesso.Paciente);

        var paciente = await _repository.GetByIdDetailsAsync(request.PacienteId);

        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var avaliacoes = await _repository.GetAvaliacoesPacienteByIdAsync(request.PacienteId);

        var response = new PacienteResultList<PacienteAvaliacaoResponse>(request.PacienteId, PacienteAvaliacaoResponse.MapFromEntity(avaliacoes));

        return Result.Success(response);
    }
}