using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoAgendamento.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoAgendamento.GetAll;

public record SelectTipoAgendamentoRequest : IRequest<Result<IReadOnlyList<TipoAgendamentoResponse>>>;