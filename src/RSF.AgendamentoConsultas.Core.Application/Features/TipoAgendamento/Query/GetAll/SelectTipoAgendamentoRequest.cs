using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Query.GetAll;

public record SelectTipoAgendamentoRequest : IRequest<Result<IReadOnlyList<TipoAgendamentoResponse>>>;