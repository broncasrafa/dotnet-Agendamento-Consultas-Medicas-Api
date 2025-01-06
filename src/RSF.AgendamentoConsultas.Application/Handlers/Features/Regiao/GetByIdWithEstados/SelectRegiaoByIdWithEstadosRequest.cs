using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetByIdWithEstados;

public record SelectRegiaoByIdWithEstadosRequest(int Id) : IRequest<Result<RegiaoResponse>>;