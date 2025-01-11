using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Tags.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Tags.GetAll;

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
