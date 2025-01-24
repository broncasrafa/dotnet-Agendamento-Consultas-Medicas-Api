using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ChangePassword;

public record ChangePasswordRequest(string NewPassword, string OldPassword) : IRequest<Result<bool>>;