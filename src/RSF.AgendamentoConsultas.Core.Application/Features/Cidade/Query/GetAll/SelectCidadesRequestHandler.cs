using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.GetAll;

public class SelectCidadesRequestHandler : IRequestHandler<SelectCidadesRequest, Result<IReadOnlyList<CidadeResponse>>>
{
    private readonly ICidadeRepository _cidadeRepository;

    public SelectCidadesRequestHandler(ICidadeRepository repository) => _cidadeRepository = repository;

    public async Task<Result<IReadOnlyList<CidadeResponse>>> Handle(SelectCidadesRequest request, CancellationToken cancellationToken)
    {
        var cidade = await _cidadeRepository.GetAllAsync();
        return Result.Success<IReadOnlyList<CidadeResponse>>(CidadeResponse.MapFromEntity(cidade));
    }
}