using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Shareable.Results;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetAllPaged;

public class SelectEspecialistaPagedRequestHandler : IRequestHandler<SelectEspecialistaPagedRequest, Result<PagedResult<EspecialistaResponse>>>
{
    private readonly IEspecialistaRepository _repository;

    public SelectEspecialistaPagedRequestHandler(IEspecialistaRepository repository) => _repository = repository;

    public async Task<Result<PagedResult<EspecialistaResponse>>> Handle(SelectEspecialistaPagedRequest request, CancellationToken cancellationToken)
    {
        var pagedResult = await _repository.GetAllPagedAsync(request.PageNum, request.PageSize);

        var response = EspecialistaResponse.MapFromEntityPaged(pagedResult, request.PageNum, request.PageSize);

        return Result.Success<PagedResult<EspecialistaResponse>>(response);
    }
}