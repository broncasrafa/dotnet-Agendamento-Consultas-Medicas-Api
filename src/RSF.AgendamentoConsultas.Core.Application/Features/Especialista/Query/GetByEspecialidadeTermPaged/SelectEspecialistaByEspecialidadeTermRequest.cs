using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Query.GetByEspecialidadeTermPaged;

public record SelectEspecialistaByEspecialidadeTermRequest(string EspecialidadeTerm, int PageSize = 15, int PageNum = 1) : IRequest<Result<PagedResult<EspecialistaResponse>>>;
