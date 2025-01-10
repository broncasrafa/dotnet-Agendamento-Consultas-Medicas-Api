using RSF.AgendamentoConsultas.Application.Handlers.Features.Agendamento.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Agendamento.Query.GetAgendamentoById;

public record SelectAgendamentoByIdRequest(int Id) : IRequest<Result<AgendamentoResponse>>;