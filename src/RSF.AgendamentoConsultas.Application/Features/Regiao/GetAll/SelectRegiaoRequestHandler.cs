using RSF.AgendamentoConsultas.Domain.Interfaces;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Regiao.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Regiao.GetAll;

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
