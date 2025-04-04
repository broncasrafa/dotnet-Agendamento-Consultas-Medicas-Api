﻿using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Command.DeletePacientePlanoMedico;

public class DeletePacientePlanoMedicoRequestHandler : IRequestHandler<DeletePacientePlanoMedicoRequest, Result<bool>>
{
    private readonly IPacienteRepository _repository;
    private readonly IBaseRepository<PacientePlanoMedico> _planoMedicoRepository;
    private readonly IHttpContextAccessor _httpContext;

    public DeletePacientePlanoMedicoRequestHandler(
        IPacienteRepository repository, 
        IBaseRepository<PacientePlanoMedico> planoMedicoRepository, 
        IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _planoMedicoRepository = planoMedicoRepository;
        _httpContext = httpContext;
    }

    public async Task<Result<bool>> Handle(DeletePacientePlanoMedicoRequest request, CancellationToken cancellationToken)
    {
        HttpContextExtensions.ValidatePermissions(_httpContext.HttpContext, request.PacienteId, ETipoPerfilAcesso.Paciente);

        var paciente = await _repository.GetByIdAsync(request.PacienteId);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o ID: '{request.PacienteId}' não foi encontrado");

        var planoMedico = await _planoMedicoRepository.GetByFilterAsync(c => c.PacienteId == request.PacienteId && c.PlanoMedicoId == request.PlanoMedicoId);
        NotFoundException.ThrowIfNull(planoMedico, $"Plano Medico com o ID: '{request.PlanoMedicoId}' não foi encontrado para o Paciente com o ID: '{request.PacienteId}'");

        planoMedico.ChangeStatus(status: false);

        var rowsAffected = await _planoMedicoRepository.ChangeStatusAsync(planoMedico);

        return await Task.FromResult(rowsAffected > 0);
    }
}