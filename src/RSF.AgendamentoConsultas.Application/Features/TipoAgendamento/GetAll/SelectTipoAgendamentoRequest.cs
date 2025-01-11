using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.TipoAgendamento.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.TipoAgendamento.GetAll;

public record SelectTipoAgendamentoRequest : IRequest<Result<IReadOnlyList<TipoAgendamentoResponse>>>;