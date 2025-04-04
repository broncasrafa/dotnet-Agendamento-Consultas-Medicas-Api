﻿using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.ConvenioMedico.Query.GetById;

public class SelectConvenioMedicoByIdRequestHandler : IRequestHandler<SelectConvenioMedicoByIdRequest, Result<ConvenioMedicoResponse>>
{
    private readonly IConvenioMedicoRepository _repository;

    public SelectConvenioMedicoByIdRequestHandler(IConvenioMedicoRepository repository) => _repository = repository;

    public async Task<Result<ConvenioMedicoResponse>> Handle(SelectConvenioMedicoByIdRequest request, CancellationToken cancellationToken)
    {
        var convenioMedico = await _repository.GetByIdAsync(request.Id);
        NotFoundException.ThrowIfNull(convenioMedico, $"Convênio Médico com o ID: '{request.Id}' não encontrado");
        return await Task.FromResult(ConvenioMedicoResponse.MapFromEntity(convenioMedico));
    }
}