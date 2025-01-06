using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoStatusConsulta.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoStatusConsulta.GetAll;

public record SelectTipoStatusConsultaRequest : IRequest<Result<IReadOnlyList<TipoStatusConsultaResponse>>>;