using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoConsulta.GetAll;

public record SelectTipoConsultaRequest : IRequest<Result<IReadOnlyList<TipoConsultaResponse>>>;