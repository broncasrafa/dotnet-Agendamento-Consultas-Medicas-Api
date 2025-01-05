using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetAll;

public record SelectRegiaoRequest : IRequest<Result<IReadOnlyList<RegiaoResponse>>>;