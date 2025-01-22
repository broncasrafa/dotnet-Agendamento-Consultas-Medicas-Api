using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetAllPaged;

public record SelectEspecialistaPagedRequest(int PageSize = 10, int PageNum = 1) : IRequest<Result<PagedResult<EspecialistaResponse>>>;