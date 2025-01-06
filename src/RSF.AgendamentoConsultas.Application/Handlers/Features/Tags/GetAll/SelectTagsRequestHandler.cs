using RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.GetAll;

public class SelectTagsRequestHandler : IRequestHandler<SelectTagsRequest, Result<IReadOnlyList<TagsResponse>>>
{
    private readonly IBaseRepository<Domain.Entities.Tags> _repository;

    public SelectTagsRequestHandler(IBaseRepository<Domain.Entities.Tags> repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<TagsResponse>>> Handle(SelectTagsRequest request, CancellationToken cancellationToken)
    {
        var regioes = await _repository.GetAllAsync();

        return Result.Success<IReadOnlyList<TagsResponse>>(TagsResponse.MapFromEntity(regioes));
    }
}
