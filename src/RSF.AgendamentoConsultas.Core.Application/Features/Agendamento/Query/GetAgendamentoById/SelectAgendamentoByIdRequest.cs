using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Agendamento.Query.GetAgendamentoById;

public record SelectAgendamentoByIdRequest(int Id) : IRequest<Result<AgendamentoResponse>>;