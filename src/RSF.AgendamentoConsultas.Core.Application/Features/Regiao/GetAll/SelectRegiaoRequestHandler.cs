using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Regiao.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Regiao.GetAll;

public class SelectRegiaoRequestHandler : IRequestHandler<SelectRegiaoRequest, Result<IReadOnlyList<RegiaoResponse>>>
{
    private readonly IRegiaoRepository _repository;

    public SelectRegiaoRequestHandler(IRegiaoRepository regiaoRepository) => _repository = regiaoRepository;

    public async Task<Result<IReadOnlyList<RegiaoResponse>>> Handle(SelectRegiaoRequest request, CancellationToken cancellationToken)
    {
        var regioes = await _repository.GetAllAsync();

        return Result.Success(RegiaoResponse.MapFromEntity(regioes));
    }
}
