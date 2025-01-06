using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.GetAll;

public record SelectTipoConsultaRequest : IRequest<Result<IReadOnlyList<TipoConsultaResponse>>>;