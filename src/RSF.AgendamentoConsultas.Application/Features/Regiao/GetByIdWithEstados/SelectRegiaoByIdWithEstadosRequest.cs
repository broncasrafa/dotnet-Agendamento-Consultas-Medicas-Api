using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Regiao.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Regiao.GetByIdWithEstados;

public record SelectRegiaoByIdWithEstadosRequest(int Id) : IRequest<Result<RegiaoResponse>>;