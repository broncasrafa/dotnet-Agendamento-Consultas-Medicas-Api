using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Login;

public record LoginRequest(string Email, string Password, string TipoAcesso) : IRequest<Result<AuthenticatedUserResponse>>;