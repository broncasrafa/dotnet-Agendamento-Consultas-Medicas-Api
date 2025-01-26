using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.DeleteEspecialidade;

public class DeleteEspecialidadeRequestHandler : IRequestHandler<DeleteEspecialidadeRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IEspecialidadeRepository _especialidadeRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaEspecialidade> _especialistaEspecialidadeRepository;

    public DeleteEspecialidadeRequestHandler(
        IEspecialistaRepository especialistaRepository, 
        IEspecialidadeRepository especialidadeRepository, 
        IBaseRepository<Domain.Entities.EspecialistaEspecialidade> especialistaEspecialidadeRepository)
    {
        _especialistaRepository = especialistaRepository;
        _especialidadeRepository = especialidadeRepository;
        _especialistaEspecialidadeRepository = especialistaEspecialidadeRepository;
    }

    public async Task<Result<bool>> Handle(DeleteEspecialidadeRequest request, CancellationToken cancellationToken)
    {
        var especialidade = await _especialidadeRepository.GetByIdAsync(request.EspecialidadeId);
        NotFoundException.ThrowIfNull(especialidade, $"Especialidade com o ID: '{request.EspecialidadeId}' não encontrada");

        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        var especialidadeAtendida = especialista.Especialidades.FirstOrDefault(c => c.EspecialidadeId == request.EspecialidadeId);
        NotFoundException.ThrowIfNull(especialidadeAtendida,
            $"Especialidade com o ID: '{request.EspecialidadeId}' não encontrada para o Especialista com o ID: '{request.EspecialistaId}'");

        var rowsAffected = await _especialistaEspecialidadeRepository.RemoveAsync(especialidadeAtendida);
        
        return await Task.FromResult(rowsAffected > 0);
    }
}