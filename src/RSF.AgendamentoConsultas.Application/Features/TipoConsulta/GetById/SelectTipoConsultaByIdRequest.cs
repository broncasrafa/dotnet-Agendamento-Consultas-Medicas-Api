using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.TipoConsulta.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.TipoConsulta.GetById;

public record SelectTipoConsultaByIdRequest(int Id) : IRequest<Result<TipoConsultaResponse>>;