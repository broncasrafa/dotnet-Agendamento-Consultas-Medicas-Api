using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmailResend;

public record ConfirmEmailResendRequest(string Email) : IRequest<Result<bool>>;