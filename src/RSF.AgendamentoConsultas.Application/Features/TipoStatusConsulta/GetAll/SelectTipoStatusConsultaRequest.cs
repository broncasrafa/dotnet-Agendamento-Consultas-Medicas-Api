using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.TipoStatusConsulta.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.TipoStatusConsulta.GetAll;

public record SelectTipoStatusConsultaRequest : IRequest<Result<IReadOnlyList<TipoStatusConsultaResponse>>>;