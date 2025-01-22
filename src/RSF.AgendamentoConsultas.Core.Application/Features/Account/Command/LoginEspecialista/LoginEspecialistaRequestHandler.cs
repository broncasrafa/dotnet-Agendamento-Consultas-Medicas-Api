using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.LoginEspecialista;

public class LoginEspecialistaRequestHandler : IRequestHandler<LoginEspecialistaRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;
    private readonly IEspecialistaRepository _especialistaRepository;

    public LoginEspecialistaRequestHandler(IAccountManagerService accountManagerService, IEspecialistaRepository especialistaRepository)
    {
        _accountManagerService = accountManagerService;
        _especialistaRepository = especialistaRepository;
    }

    public async Task<Result<AuthenticatedUserResponse>> Handle(LoginEspecialistaRequest request, CancellationToken cancellationToken)
    {
        var usuarioAutenticado = await _accountManagerService.LoginAsync(request.Email, request.Password);

        var especialista = await _especialistaRepository.GetByEmailAsync(request.Email);
        NotFoundException.ThrowIfNull(especialista, $"Especialista com o e-mail '{request.Email}' não encontrado");

        var response = new AuthenticatedUserResponse(new Credentials(usuarioAutenticado.Token), profissional: EspecialistaResponse.MapFromEntity(especialista));

        return await Task.FromResult(response);
    }
}