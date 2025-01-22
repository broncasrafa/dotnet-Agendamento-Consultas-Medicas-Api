using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.LoginAdmin;

public class LoginAdminRequestHandler : IRequestHandler<LoginAdminRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;

    public LoginAdminRequestHandler(IAccountManagerService accountManagerService) => _accountManagerService = accountManagerService;

    public async Task<Result<AuthenticatedUserResponse>> Handle(LoginAdminRequest request, CancellationToken cancellationToken)
    {
        var usuarioAutenticado = await _accountManagerService.LoginAsync(request.Email, request.Password);

        var response = new AuthenticatedUserResponse(new Credentials(usuarioAutenticado.Token), usuario: usuarioAutenticado);

        return await Task.FromResult(response);
    }
}