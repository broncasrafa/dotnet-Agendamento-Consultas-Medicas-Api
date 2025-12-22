using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Query.GetPerguntaById;

public class SelectPerguntaByIdRequestHandler : IRequestHandler<SelectPerguntaByIdRequest, Result<PerguntaResponse>>
{
    private readonly IPerguntaRepository _perguntaRepository;
    private readonly IHttpContextAccessor _httpContext;

    public SelectPerguntaByIdRequestHandler(IPerguntaRepository perguntaRepository, IHttpContextAccessor httpContext)
    {
        _perguntaRepository = perguntaRepository;
        _httpContext = httpContext;
    }

    public async Task<Result<PerguntaResponse>> Handle(SelectPerguntaByIdRequest request, CancellationToken cancellationToken)
    {
        var pergunta = await _perguntaRepository.GetByIdAsync(request.PerguntaId);
        int? pacienteId = HttpContextExtensions.GetUserResourceIdFromClaims(_httpContext.HttpContext);
        pacienteId = pacienteId == 0 ? null : pacienteId;

        NotFoundException.ThrowIfNull(pergunta, $"Pergunta com o ID: '{request.PerguntaId}' não encontrada");

        return await Task.FromResult(PerguntaResponse.MapFromEntity(pergunta, pacienteId));
    }
}