using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Register;

public class RegisterRequestHandler(IAccountManagerService accountManagerService) : IRequestHandler<RegisterRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService = accountManagerService;

    public async Task<Result<AuthenticatedUserResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsAsync(request.Email);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        var result = await _accountManagerService.RegisterAsync(request.NomeCompleto, request.CPF, request.Username, request.Email, request.Telefone, request.Password, request.TipoAcesso);

        var response = new AuthenticatedUserResponse(usuario: result);

        return await Task.FromResult(response);
    }
}
