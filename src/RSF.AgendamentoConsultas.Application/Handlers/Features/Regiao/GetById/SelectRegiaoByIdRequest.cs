using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetById;

public record SelectRegiaoByIdRequest(int Id): IRequest<Result<RegiaoResponse>>;