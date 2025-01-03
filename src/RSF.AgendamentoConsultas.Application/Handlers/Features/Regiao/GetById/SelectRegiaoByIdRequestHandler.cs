using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetAll;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetById;

public class SelectRegiaoByIdRequestHandler : IRequestHandler<SelectRegiaoByIdRequest, Result<SelectRegiaoResponse>>
{
    private readonly IRegiaoRepository _repository;

    public SelectRegiaoByIdRequestHandler(IRegiaoRepository regiaoRepository) => _repository = regiaoRepository;

    public async Task<Result<SelectRegiaoResponse>> Handle(SelectRegiaoByIdRequest request, CancellationToken cancellationToken)
    {
        var regiao = await _repository.GetByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(regiao, $"Região com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(SelectRegiaoResponse.MapFromEntity(regiao));
    }
}