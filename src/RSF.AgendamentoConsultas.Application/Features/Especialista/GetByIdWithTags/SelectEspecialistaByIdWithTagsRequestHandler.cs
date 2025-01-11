using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByIdWithTags;

public class SelectEspecialistaByIdWithTagsRequestHandler : IRequestHandler<SelectEspecialistaByIdWithTagsRequest, Result<EspecialistaResultList<EspecialistaTagsResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByIdWithTagsRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<EspecialistaResultList<EspecialistaTagsResponse>>> Handle(SelectEspecialistaByIdWithTagsRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByIdWithTagsAsync(request.Id);

        NotFoundException.ThrowIfNull(data, $"Especialista com o ID: '{request.Id}' não encontrado");

        var list = EspecialistaTagsResponse.MapFromEntity(data.Tags?.Select(c => c.Tag));

        var response = new EspecialistaResultList<EspecialistaTagsResponse>(data.EspecialistaId, list);

        return Result.Success(response);
    }
}