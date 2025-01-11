using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.TipoAgendamento.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.TipoAgendamento.GetById;

public record SelectTipoAgendamentoByIdRequest(int Id) : IRequest<Result<TipoAgendamentoResponse>>;