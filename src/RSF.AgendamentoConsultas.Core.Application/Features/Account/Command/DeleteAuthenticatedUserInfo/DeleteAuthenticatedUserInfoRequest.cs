using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.DeleteAuthenticatedUserInfo;

public record DeleteAuthenticatedUserInfoRequest : IRequest<Result<bool>>;