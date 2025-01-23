using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterPaciente;

public class RegisterPacienteRequestHandler: IRequestHandler<RegisterPacienteRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;
    private readonly IPacienteRepository _pacienteRepository;

    public RegisterPacienteRequestHandler(
        IAccountManagerService accountManagerService, 
        IPacienteRepository pacienteRepository)
    {
        _accountManagerService = accountManagerService;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<AuthenticatedUserResponse>> Handle(RegisterPacienteRequest request, CancellationToken cancellationToken)
    {
        var isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(c => c.Documento == request.CPF);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(c => c.Email == request.Email);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        var newUser = await _accountManagerService.RegisterAsync(request.NomeCompleto, request.CPF, request.Username, request.Email, request.Telefone, request.Genero, request.Password, ETipoPerfilAcesso.Paciente);

        var newPaciente = new Domain.Entities.Paciente(
            nome: request.NomeCompleto,
            cpf: request.CPF,
            email: request.Email,
            telefone: request.Telefone,
            genero: request.Genero,
            dataNascimento: request.DataNascimento.ToString("yyyy-MM-dd"),
            termoUsoAceito: true
        );
        await _pacienteRepository.AddAsync(newPaciente);

        var response = new AuthenticatedUserResponse(usuario: newUser);
        return await Task.FromResult(response);
    }
}
