using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Query.GetById;

public record SelectTipoStatusConsultaByIdRequest(int Id) : IRequest<Result<TipoStatusConsultaResponse>>;