using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.LoginConsultor;

public record LoginConsultorRequest(string Email, string Password) : IRequest<Result<AuthenticatedUserResponse>>;