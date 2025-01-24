using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.UpdateAuthenticatedUserInfo;

public record UpdateAuthenticatedUserInfoRequest
(
    string NomeCompleto,
    string Telefone,
    string Email,
    string Username
) : IRequest<Result<bool>>;