using RSF.AgendamentoConsultas.Application.Handlers.Features.EspecialidadeGrupo.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.EspecialidadeGrupo.GetAll;

public class SelectEspecialidadeGrupoRequestHandler : IRequestHandler<SelectEspecialidadeGrupoRequest, Result<IReadOnlyList<EspecialidadeGrupoResponse>>>
{
    private readonly IBaseRepository<Domain.Entities.EspecialidadeGrupo> _repository;

    public SelectEspecialidadeGrupoRequestHandler(IBaseRepository<Domain.Entities.EspecialidadeGrupo> repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<EspecialidadeGrupoResponse>>> Handle(SelectEspecialidadeGrupoRequest request, CancellationToken cancellationToken)
    {
        var lista = await _repository.GetAllAsync();
        return Result.Success<IReadOnlyList<EspecialidadeGrupoResponse>>(EspecialidadeGrupoResponse.MapFromEntity(lista));
    }
}