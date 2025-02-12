using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Regiao.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Regiao.Query.GetAll;

public record SelectRegiaoRequest : IRequest<Result<IReadOnlyList<RegiaoResponse>>>;