using RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.Responses;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.GetById;

public class SelectTagsByIdRequestHandler : IRequestHandler<SelectTagsByIdRequest, Result<TagsResponse>>
{
    private readonly IBaseRepository<Domain.Entities.Tags> _repository;

    public SelectTagsByIdRequestHandler(IBaseRepository<Domain.Entities.Tags> repository) => _repository = repository;

    public async Task<Result<TagsResponse>> Handle(SelectTagsByIdRequest request, CancellationToken cancellationToken)
    {
        var Tags = await _repository.GetByIdAsync(request.Id);

        NotFoundException.ThrowIfNull(Tags, $"Tag com o ID: '{request.Id}' não encontrada");

        return await Task.FromResult(TagsResponse.MapFromEntity(Tags));
    }
}