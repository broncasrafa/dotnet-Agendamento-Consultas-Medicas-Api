using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetAllPaged;

public class SelectEspecialistaPagedRequestHandler : IRequestHandler<SelectEspecialistaPagedRequest, Result<PagedResult<EspecialistaResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaPagedRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<PagedResult<EspecialistaResponse>>> Handle(SelectEspecialistaPagedRequest request, CancellationToken cancellationToken)
    {
        var pagedResult = await _repository.GetAllPagedAsync(request.PageNum, request.PageSize);

        var response = EspecialistaResponse.MapFromEntityPaged(pagedResult, request.PageNum, request.PageSize);

        return Result.Success(response);
    }
}