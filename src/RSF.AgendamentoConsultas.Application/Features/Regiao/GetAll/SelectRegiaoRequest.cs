using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Regiao.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Regiao.GetAll;

public record SelectRegiaoRequest : IRequest<Result<IReadOnlyList<RegiaoResponse>>>;