using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Regiao.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Regiao.GetById;

public record SelectRegiaoByIdRequest(int Id) : IRequest<Result<RegiaoResponse>>;