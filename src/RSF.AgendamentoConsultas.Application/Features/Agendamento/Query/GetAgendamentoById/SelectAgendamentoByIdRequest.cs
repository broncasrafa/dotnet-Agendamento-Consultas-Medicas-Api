using MediatR;

using OperationResult;

using RSF.AgendamentoConsultas.Application.Features.Agendamento.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Agendamento.Query.GetAgendamentoById;

public record SelectAgendamentoByIdRequest(int Id) : IRequest<Result<AgendamentoResponse>>;