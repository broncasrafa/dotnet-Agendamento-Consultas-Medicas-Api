using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoStatusConsulta.GetById;

public record SelectTipoStatusConsultaByIdRequest(int Id) : IRequest<Result<TipoStatusConsultaResponse>>;