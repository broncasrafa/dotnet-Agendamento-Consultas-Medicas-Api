using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.TipoStatusConsulta.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.TipoStatusConsulta.GetById;

public record SelectTipoStatusConsultaByIdRequest(int Id) : IRequest<Result<TipoStatusConsultaResponse>>;