using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ResetPassword;

public record ResetPasswordRequest(string Email, string ResetCode, string NewPassword) : IRequest<Result<bool>>;
