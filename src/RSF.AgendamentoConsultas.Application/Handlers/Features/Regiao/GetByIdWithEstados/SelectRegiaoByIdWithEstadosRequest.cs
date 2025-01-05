using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetByIdWithEstados;

public record SelectRegiaoByIdWithEstadosRequest(int Id) : IRequest<Result<RegiaoResponse>>;