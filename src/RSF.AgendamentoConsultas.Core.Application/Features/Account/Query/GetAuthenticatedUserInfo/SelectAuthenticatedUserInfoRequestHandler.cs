using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Query.GetAuthenticatedUserInfo;

public class SelectAuthenticatedUserInfoRequestHandler : IRequestHandler<SelectAuthenticatedUserInfoRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;
    private readonly IHttpContextAccessor _httpContext;

    public SelectAuthenticatedUserInfoRequestHandler(IAccountManagerService accountManagerService, IHttpContextAccessor httpContext)
    {
        _accountManagerService = accountManagerService;
        _httpContext = httpContext;
    }


    public async Task<Result<AuthenticatedUserResponse>> Handle(SelectAuthenticatedUserInfoRequest request, CancellationToken cancellationToken)
    {
        var authenticatedUser = _httpContext.HttpContext.User;
        UnauthorizedRequestException.ThrowIfNotAuthenticated(authenticatedUser.Identity!.IsAuthenticated, "Usuário não está autenticado na plataforma");

        var user = await _accountManagerService.GetUserAsync(authenticatedUser);
        NotFoundException.ThrowIfNull(user, "Usuário não está autenticado na plataforma");

        var response = new AuthenticatedUserResponse(usuario: UsuarioAutenticadoModel.MapFromEntity(user));

        return await Task.FromResult(response);
    }
}