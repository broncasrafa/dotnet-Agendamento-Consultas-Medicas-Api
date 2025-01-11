using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.TipoConsulta.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.TipoConsulta.GetAll;

public record SelectTipoConsultaRequest : IRequest<Result<IReadOnlyList<TipoConsultaResponse>>>;