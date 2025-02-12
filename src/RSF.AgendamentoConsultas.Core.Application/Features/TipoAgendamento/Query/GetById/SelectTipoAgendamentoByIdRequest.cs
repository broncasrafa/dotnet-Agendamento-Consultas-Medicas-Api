using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Query.GetById;

public record SelectTipoAgendamentoByIdRequest(int Id) : IRequest<Result<TipoAgendamentoResponse>>;