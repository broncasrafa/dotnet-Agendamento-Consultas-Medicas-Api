using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using OperationResult;
using MediatR;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByFilterPaged;

public class SelectEspecialistaByFiltersPagedRequestHandler : IRequestHandler<SelectEspecialistaByFiltersPagedRequest, Result<PagedResult<EspecialistaResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaByFiltersPagedRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<PagedResult<EspecialistaResponse>>> Handle(SelectEspecialistaByFiltersPagedRequest request, CancellationToken cancellationToken)
    {
        var pagedResult = await _repository.GetAllByFiltersPagedAsync(request.EspecialidadeId, request.Cidade, request.PageNum, request.PageSize);

        var response = EspecialistaResponse.MapFromEntityPaged(pagedResult, request.PageNum, request.PageSize);

        return Result.Success(response);
    }
}