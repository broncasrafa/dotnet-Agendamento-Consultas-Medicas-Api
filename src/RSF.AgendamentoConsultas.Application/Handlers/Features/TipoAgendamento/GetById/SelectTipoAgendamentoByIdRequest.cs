using RSF.AgendamentoConsultas.Application.Handlers.Features.TipoAgendamento.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoAgendamento.GetById;

public record SelectTipoAgendamentoByIdRequest(int Id): IRequest<Result<TipoAgendamentoResponse>>;