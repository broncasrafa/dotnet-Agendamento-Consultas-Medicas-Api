using RSF.AgendamentoConsultas.Shareable.Results;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.GetByNamePaged;

public record SelectEspecialistaByNamePagedRequest(string Name, int PageSize = 10, int PageNum = 1) : IRequest<Result<PagedResult<EspecialistaResponse>>>;
