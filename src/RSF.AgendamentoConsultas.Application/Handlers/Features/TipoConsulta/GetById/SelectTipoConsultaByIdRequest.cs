using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.GetById;

public record SelectTipoConsultaByIdRequest(int Id): IRequest<Result<TipoConsultaResponse>>;