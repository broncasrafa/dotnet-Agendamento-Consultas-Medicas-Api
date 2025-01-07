﻿using RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.GetByIdWithCidadesAtendidasPorEstado;

public class SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequestHandler : IRequestHandler<SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequest, Result<ConvenioMedicoResponse>>
{
    private readonly IConvenioMedicoRepository _repository;

    public SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequestHandler(IConvenioMedicoRepository repository) => _repository = repository;

    public async Task<Result<ConvenioMedicoResponse>> Handle(SelectConvenioMedicoByIdWithCidadesAtendidasPorEstadoRequest request, CancellationToken cancellationToken)
    {
        var convenioMedico = await _repository.GetByIdWithCidadesAtendidasAsync(request.Id, request.EstadoId);
        NotFoundException.ThrowIfNull(convenioMedico, $"Convênio Médico com o ID: '{request.Id}' não encontrado");
        return await Task.FromResult(ConvenioMedicoResponse.MapFromEntity(convenioMedico));
    }
}