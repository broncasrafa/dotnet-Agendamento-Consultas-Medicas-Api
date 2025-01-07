using RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Shareable.Results;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.GetAllPaged;

public record SelectEspecialistaPagedRequest(int PageSize = 10, int PageNum = 1) : IRequest<Result<PagedResult<EspecialistaResponse>>>;