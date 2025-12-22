using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByEspecialidadeTermPaged;

public class SelectEspecialistaByEspecialidadeTermRequestHandler : IRequestHandler<SelectEspecialistaByEspecialidadeTermRequest, Result<PagedResult<EspecialistaResponse>>>
{
    private readonly IEspecialistaRepository _especialistaRepository;

    public SelectEspecialistaByEspecialidadeTermRequestHandler(IEspecialistaRepository especialistaRepository)
        => _especialistaRepository = especialistaRepository;

    public async Task<Result<PagedResult<EspecialistaResponse>>> Handle(SelectEspecialistaByEspecialidadeTermRequest request, CancellationToken cancellationToken)
    {
        var pagedResult = await _especialistaRepository.GetAllByEspecialidadeTermPagedAsync(request.EspecialidadeTerm, request.PageNum, request.PageSize);

        var response = EspecialistaResponse.MapFromEntityPaged(pagedResult, request.PageNum, request.PageSize);

        return Result.Success(response);
    }
}