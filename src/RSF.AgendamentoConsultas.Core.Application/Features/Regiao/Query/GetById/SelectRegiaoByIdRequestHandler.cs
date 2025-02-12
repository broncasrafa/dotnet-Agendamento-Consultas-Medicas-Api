using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Regiao.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Regiao.Query.GetById;

public class SelectRegiaoByIdRequestHandler : IRequestHandler<SelectRegiaoByIdRequest, Result<RegiaoResponse>>
{
    private readonly IRegiaoRepository _repository;

    public SelectRegiaoByIdRequestHandler(IRegiaoRepository regiaoRepository) => _repository = regiaoRepository;

    public async Task<Result<RegiaoResponse>> Handle(SelectRegiaoByIdRequest request, CancellationToken cancellationToken)
    {
        var regiao = await _repository.GetByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(regiao, $"Região com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(RegiaoResponse.MapFromEntity(regiao));
    }
}