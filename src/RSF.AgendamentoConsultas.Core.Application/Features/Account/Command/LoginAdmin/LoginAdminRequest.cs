using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.LoginAdmin;

public record LoginAdminRequest(string Email, string Password) : IRequest<Result<AuthenticatedUserResponse>>;