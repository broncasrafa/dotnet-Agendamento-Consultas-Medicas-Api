using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Especialidade.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialidade.GetAll;

public class SelectEspecialidadeRequestHandler : IRequestHandler<SelectEspecialidadeRequest, Result<IReadOnlyList<EspecialidadeResponse>>>
{
    private readonly IEspecialidadeRepository _repository;

    public SelectEspecialidadeRequestHandler(IEspecialidadeRepository repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<EspecialidadeResponse>>> Handle(SelectEspecialidadeRequest request, CancellationToken cancellationToken)
    {
        var lista = await _repository.GetAllAsync();
        return Result.Success<IReadOnlyList<EspecialidadeResponse>>(EspecialidadeResponse.MapFromEntity(lista));
    }
}