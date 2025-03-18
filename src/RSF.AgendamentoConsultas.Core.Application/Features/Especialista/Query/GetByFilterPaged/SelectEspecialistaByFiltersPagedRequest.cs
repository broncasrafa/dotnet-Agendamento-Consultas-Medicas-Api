using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByFilterPaged;

public record SelectEspecialistaByFiltersPagedRequest(
    int? EspecialidadeId,
    string Cidade,
    int PageSize = 10,
    int PageNum = 1) : IRequest<Result<PagedResult<EspecialistaResponse>>>;