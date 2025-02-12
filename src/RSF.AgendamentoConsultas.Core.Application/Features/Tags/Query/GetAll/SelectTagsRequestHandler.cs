using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Tags.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Tags.Query.GetAll;

public class SelectTagsRequestHandler : IRequestHandler<SelectTagsRequest, Result<IReadOnlyList<TagsResponse>>>
{
    private readonly IBaseRepository<Domain.Entities.Tags> _repository;

    public SelectTagsRequestHandler(IBaseRepository<Domain.Entities.Tags> repository) => _repository = repository;

    public async Task<Result<IReadOnlyList<TagsResponse>>> Handle(SelectTagsRequest request, CancellationToken cancellationToken)
    {
        var regioes = await _repository.GetAllAsync();

        return Result.Success(TagsResponse.MapFromEntity(regioes));
    }
}
