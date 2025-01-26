﻿using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.UpdateEspecialidade;

public class UpdateEspecialidadeRequestHandler : IRequestHandler<UpdateEspecialidadeRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IEspecialidadeRepository _especialidadeRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaEspecialidade> _especialistaEspecialidadeRepository;

    public UpdateEspecialidadeRequestHandler(
        IEspecialistaRepository especialistaRepository,
        IEspecialidadeRepository especialidadeRepository,
        IBaseRepository<Domain.Entities.EspecialistaEspecialidade> especialistaEspecialidadeRepository)
    {
        _especialistaRepository = especialistaRepository;
        _especialidadeRepository = especialidadeRepository;
        _especialistaEspecialidadeRepository = especialistaEspecialidadeRepository;
    }

    public async Task<Result<bool>> Handle(UpdateEspecialidadeRequest request, CancellationToken cancellationToken)
    {
        var especialidade = await _especialidadeRepository.GetByIdAsync(request.EspecialidadeId);
        NotFoundException.ThrowIfNull(especialidade, $"Especialidade com o ID: '{request.EspecialidadeId}' não encontrada");

        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var especialidadeAtendida = especialista.Especialidades.FirstOrDefault(c => c.Id == request.Id);
        NotFoundException.ThrowIfNull(especialidadeAtendida,
            $"Especialidade com o ID de registro: '{request.Id}' não encontrada para o Especialista com o ID: '{request.EspecialistaId}'");

        especialidadeAtendida!.Update(request.EspecialidadeId, request.TipoEspecialidade);

        var rowsAffected = await _especialistaEspecialidadeRepository.UpdateAsync(especialidadeAtendida);

        return await Task.FromResult(rowsAffected > 0);
    }
}