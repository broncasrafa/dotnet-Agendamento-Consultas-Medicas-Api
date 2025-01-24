using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Query.GetAuthenticatedUserInfo;

public record SelectAuthenticatedUserInfoRequest : IRequest<Result<AuthenticatedUserResponse>>;