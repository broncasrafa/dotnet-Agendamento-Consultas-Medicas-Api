using RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Query.SearchByName;

public class SelectCidadesByNameRequestHandler : IRequestHandler<SelectCidadesByNameRequest, Result<IReadOnlyList<CidadeResponse>>>
{
    private readonly ICidadeRepository _cidadeRepository;

    public SelectCidadesByNameRequestHandler(ICidadeRepository repository) => _cidadeRepository = repository;

    public async Task<Result<IReadOnlyList<CidadeResponse>>> Handle(SelectCidadesByNameRequest request, CancellationToken cancellationToken)
    {
        var cidade = await _cidadeRepository.GetAllByNameAsync(request.Name);
        NotFoundException.ThrowIfNull(cidade, $"Cidades com o termo de busca: '{request.Name}' não encontradas");
        return Result.Success<IReadOnlyList<CidadeResponse>>(CidadeResponse.MapFromEntity(cidade));
    }
}