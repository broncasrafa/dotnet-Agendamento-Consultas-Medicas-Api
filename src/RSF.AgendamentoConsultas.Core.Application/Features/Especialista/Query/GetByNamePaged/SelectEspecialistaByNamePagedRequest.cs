using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByNamePaged;

public record SelectEspecialistaByNamePagedRequest(string Name, int PageSize = 10, int PageNum = 1) : IRequest<Result<PagedResult<EspecialistaResponse>>>;
