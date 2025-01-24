using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmail;

public record ConfirmEmailRequest(string UserId, string Code) : IRequest<Result<bool>>;