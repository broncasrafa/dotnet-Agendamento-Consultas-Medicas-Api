using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.GetByName;

public class SelectEspecialidadeByNameRequestHandler : IRequestHandler<SelectEspecialidadeByNameRequest, Result<IReadOnlyList<EspecialidadeResponse>>>
{
    private readonly IEspecialidadeRepository _repository;

    public SelectEspecialidadeByNameRequestHandler(IEspecialidadeRepository repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<EspecialidadeResponse>>> Handle(SelectEspecialidadeByNameRequest request, CancellationToken cancellationToken)
    {
        var lista = await _repository.GetByNameAsync(request.Name);
        return Result.Success<IReadOnlyList<EspecialidadeResponse>>(EspecialidadeResponse.MapFromEntity(lista));
    }
}