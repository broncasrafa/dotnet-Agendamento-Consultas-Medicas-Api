using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Command.AddEspecialidade;

public class AddEspecialidadeRequestHandler : IRequestHandler<AddEspecialidadeRequest, Result<bool>>
{
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IEspecialidadeRepository _especialidadeRepository;
    private readonly IBaseRepository<Domain.Entities.EspecialistaEspecialidade> _especialistaEspecialidadeRepository;

    public AddEspecialidadeRequestHandler(
        IEspecialistaRepository especialistaRepository,
        IEspecialidadeRepository especialidadeRepository,
        IBaseRepository<Domain.Entities.EspecialistaEspecialidade> especialistaEspecialidadeRepository)
    {
        _especialistaRepository = especialistaRepository;
        _especialidadeRepository = especialidadeRepository;
        _especialistaEspecialidadeRepository = especialistaEspecialidadeRepository;
    }

    public async Task<Result<bool>> Handle(AddEspecialidadeRequest request, CancellationToken cancellationToken)
    {
        var especialidade = await _especialidadeRepository.GetByIdAsync(request.EspecialidadeId);
        NotFoundException.ThrowIfNull(especialidade, $"Especialidade com o ID: '{request.EspecialidadeId}' não encontrada");

        var especialista = await _especialistaRepository.GetByIdAsync(request.EspecialistaId);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.EspecialistaId}' não encontrado");

        AlreadyExistsException.ThrowIfExists(
            especialista.Especialidades.Any(c => c.EspecialidadeId == request.EspecialidadeId),
            $"Especialidade com o ID: '{request.EspecialidadeId}' já atendida pelo Especialista com o ID: '{request.EspecialistaId}'");

        var newEspecialidade = new Domain.Entities.EspecialistaEspecialidade(request.EspecialistaId, request.EspecialidadeId, request.TipoEspecialidade);
        especialista.Especialidades.Add(newEspecialidade);

        var rowsAffected = await _especialistaEspecialidadeRepository.AddAsync(newEspecialidade);

        return await Task.FromResult(rowsAffected > 0);
    }
}