using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.Query.GetById;

public record SelectTipoConsultaByIdRequest(int Id) : IRequest<Result<TipoConsultaResponse>>;