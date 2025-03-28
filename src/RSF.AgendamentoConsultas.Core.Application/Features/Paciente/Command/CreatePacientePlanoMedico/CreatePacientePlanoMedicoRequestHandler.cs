﻿using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.CreatePacientePlanoMedico;

public class CreatePacientePlanoMedicoRequestHandler : IRequestHandler<CreatePacientePlanoMedicoRequest, Result<PacientePlanoMedicoResponse>>
{
    private readonly IPacienteRepository _repository;
    private readonly IConvenioMedicoRepository _convenioMedicoRepository;
    private readonly IBaseRepository<PacientePlanoMedico> _pacientePlanoMedicoRepository;

    public CreatePacientePlanoMedicoRequestHandler(
        IPacienteRepository repository,
        IConvenioMedicoRepository convenioMedicoRepository,
        IBaseRepository<PacientePlanoMedico> pacientePlanoMedicoRepository)
    {
        _repository = repository;
        _convenioMedicoRepository = convenioMedicoRepository;
        _pacientePlanoMedicoRepository = pacientePlanoMedicoRepository;
    }

    public async Task<Result<PacientePlanoMedicoResponse>> Handle(CreatePacientePlanoMedicoRequest request, CancellationToken cancellationToken)
    {
        var convenioMedico = await _convenioMedicoRepository.GetByIdAsync(request.ConvenioMedicoId);
        NotFoundException.ThrowIfNull(convenioMedico, $"Convênio Médico com o ID: '{request.ConvenioMedicoId}' não encontrado");

        var paciente = await _repository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var planoMedico = new PacientePlanoMedico(request.NomePlano, request.NumCartao, request.PacienteId, request.ConvenioMedicoId);
        
        await _pacientePlanoMedicoRepository.AddAsync(planoMedico);
        
        planoMedico.ConvenioMedico = convenioMedico;

        return await Task.FromResult(PacientePlanoMedicoResponse.MapFromEntity(planoMedico));
    }
}