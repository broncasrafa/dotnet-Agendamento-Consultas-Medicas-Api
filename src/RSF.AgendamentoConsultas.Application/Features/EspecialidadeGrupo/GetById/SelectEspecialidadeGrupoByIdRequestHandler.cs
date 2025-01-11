using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.EspecialidadeGrupo.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.EspecialidadeGrupo.GetById;

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