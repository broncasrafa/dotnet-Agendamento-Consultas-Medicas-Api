using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetAll;

public record SelectRegiaoRequest : IRequest<Result<IReadOnlyList<RegiaoResponse>>>;