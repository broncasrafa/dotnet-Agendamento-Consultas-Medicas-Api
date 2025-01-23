using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterEspecialista;

public class RegisterEspecialistaRequestHandler: IRequestHandler<RegisterEspecialistaRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;
    private readonly IEspecialistaRepository _especialistaRepository;

    public RegisterEspecialistaRequestHandler(IAccountManagerService accountManagerService, IEspecialistaRepository especialistaRepository)
    {
        _accountManagerService = accountManagerService;
        _especialistaRepository = especialistaRepository;
    }

    public async Task<Result<AuthenticatedUserResponse>> Handle(RegisterEspecialistaRequest request, CancellationToken cancellationToken)
    {
        var isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(c => c.Email == request.Email);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(c => c.Documento == request.Licenca);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        var newUser = await _accountManagerService.RegisterAsync(request.NomeCompleto, request.Licenca, request.Username, request.Email, request.Telefone, request.Genero, request.Password, ETipoPerfilAcesso.Profissional);

        var newEspecialista = new Domain.Entities.Especialista(
            nome: request.NomeCompleto, 
            licenca: request.Licenca, 
            email: request.Email, 
            genero: request.Genero);

        await _especialistaRepository.AddAsync(newEspecialista);

        var response = new AuthenticatedUserResponse(usuario: newUser);        
        return await Task.FromResult(response);
    }
}