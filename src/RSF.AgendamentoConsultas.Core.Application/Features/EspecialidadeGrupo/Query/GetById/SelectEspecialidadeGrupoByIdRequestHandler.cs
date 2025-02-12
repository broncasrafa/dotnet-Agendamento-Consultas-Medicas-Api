using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

namespace RSF.AgendamentoConsultas.Core.Application.Features.EspecialidadeGrupo.Query.GetById;

public class SelectEspecialidadeGrupoByIdRequestHandler : IRequestHandler<SelectEspecialidadeGrupoByIdRequest, Result<EspecialidadeGrupoResponse>>
{
    private readonly IBaseRepository<Domain.Entities.EspecialidadeGrupo> _repository;

    public SelectEspecialidadeGrupoByIdRequestHandler(IBaseRepository<Domain.Entities.EspecialidadeGrupo> repository) => _repository = repository;

    public async Task<Result<EspecialidadeGrupoResponse>> Handle(SelectEspecialidadeGrupoByIdRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdAsync(request.Id);
        NotFoundException.ThrowIfNull(data, $"Grupo da Especialidade com o ID: '{request.Id}' não encontrado");
        return await Task.FromResult(EspecialidadeGrupoResponse.MapFromEntity(data));
    }
}