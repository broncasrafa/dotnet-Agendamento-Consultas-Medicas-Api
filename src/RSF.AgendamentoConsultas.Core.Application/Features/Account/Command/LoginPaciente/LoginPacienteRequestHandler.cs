using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.LoginPaciente;

public class LoginPacienteRequestHandler : IRequestHandler<LoginPacienteRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;
    private readonly IPacienteRepository _pacienteRepository;

    public LoginPacienteRequestHandler(IAccountManagerService accountManagerService, IPacienteRepository pacienteRepository)
    {
        _accountManagerService = accountManagerService;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<AuthenticatedUserResponse>> Handle(LoginPacienteRequest request, CancellationToken cancellationToken)
    {
        var usuarioAutenticado = await _accountManagerService.LoginAsync(request.Email, request.Password);

        var paciente = await _pacienteRepository.GetByEmailAsync(request.Email);
        NotFoundException.ThrowIfNull(paciente, $"Paciente com o e-mail '{request.Email}' não encontrado");

        var response = new AuthenticatedUserResponse(new Credentials(usuarioAutenticado.Token), paciente: PacienteResponse.MapFromEntity(paciente));

        return await Task.FromResult(response);
    }
}