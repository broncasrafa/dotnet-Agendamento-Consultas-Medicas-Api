using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Regiao.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Regiao.Query.GetByIdWithEstados;

public record SelectRegiaoByIdWithEstadosRequest(int Id) : IRequest<Result<RegiaoResponse>>;