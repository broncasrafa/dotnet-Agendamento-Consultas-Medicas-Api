﻿using RSF.AgendamentoConsultas.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Query.GetRespostasById;

public class SelectRespostaByIdRequestHandler : IRequestHandler<SelectRespostaByIdRequest, Result<RespostaResponse>>
{
    private readonly IPerguntaRespostaRepository _perguntaRespostaRepository;

    public SelectRespostaByIdRequestHandler(IPerguntaRespostaRepository perguntaRespostaRepository)
        => _perguntaRespostaRepository = perguntaRespostaRepository;

    public async Task<Result<RespostaResponse>> Handle(SelectRespostaByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _perguntaRespostaRepository.GetByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(result, $"Resposta com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(RespostaResponse.MapFromEntity(result));
    }
}