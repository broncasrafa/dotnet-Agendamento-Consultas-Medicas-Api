using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Register;

public class RegisterRequestHandler: IRequestHandler<RegisterRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;

    public RegisterRequestHandler(IAccountManagerService accountManagerService) => _accountManagerService = accountManagerService;

    public async Task<Result<AuthenticatedUserResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(c => c.Documento == request.CPF);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        var perfil = Utilitarios.ConverterTipoAcessoParaRoleEnum(request.TipoAcesso);

        var result = await _accountManagerService.RegisterAsync(request.NomeCompleto, request.CPF, request.Username, request.Email, request.Telefone, request.Genero, request.Password, perfil);

        var response = new AuthenticatedUserResponse(usuario: result);

        return await Task.FromResult(response);
    }
}