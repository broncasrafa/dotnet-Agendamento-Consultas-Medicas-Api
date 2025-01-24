using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ForgotPassword;

public record ForgotPasswordRequest(string Email) : IRequest<Result<bool>>;