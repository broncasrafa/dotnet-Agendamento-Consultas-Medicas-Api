using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByIdWithTags;

public class SelectEspecialistaByIdWithTagsRequestHandler : IRequestHandler<SelectEspecialistaByIdWithTagsRequest, Result<EspecialistaResultList<EspecialistaTagsResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByIdWithTagsRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResultList<EspecialistaTagsResponse>>> Handle(SelectEspecialistaByIdWithTagsRequest request, CancellationToken cancellationToken)
    {
        var especialista = await _repository.GetByIdAsync(request.Id);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o ID: '{request.Id}' não encontrado");

        var marcacoes = await _repository.GetAllMarcacoesEspecialistaByIdAsync(request.Id);

        var list = EspecialistaTagsResponse.MapFromEntity(marcacoes);
        var response = new EspecialistaResultList<EspecialistaTagsResponse>(especialista.EspecialistaId, list);

        return Result.Success(response);
    }
}