using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Login;

public class LoginRequestHandler : IRequestHandler<LoginRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEspecialistaRepository _especialistaRepository;

    public LoginRequestHandler(
        IAccountManagerService accountManagerService, 
        IPacienteRepository pacienteRepository, 
        IEspecialistaRepository especialistaRepository)
    {
        _accountManagerService = accountManagerService;
        _pacienteRepository = pacienteRepository;
        _especialistaRepository = especialistaRepository;
    }

    public async Task<Result<AuthenticatedUserResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var usuarioAutenticado = await _accountManagerService.LoginAsync(request.Email, request.Password);

        Domain.Entities.Paciente paciente;
        Domain.Entities.Especialista especialista;
        AuthenticatedUserResponse response;

        if (request.TipoAcesso.ToUpperInvariant().Equals(ETipoPerfilAcesso.Paciente.ToString().ToUpperInvariant()))
        {
            paciente = await _pacienteRepository.GetByEmailAsync(request.Email);
            NotFoundException.ThrowIfNull(paciente, $"Paciente com o e-mail '{request.Email}' não encontrado");
            response = new AuthenticatedUserResponse(new Credentials(usuarioAutenticado.Token), paciente: PacienteResponse.MapFromEntity(paciente));
        }
        else if (request.TipoAcesso.ToUpperInvariant().Equals(ETipoPerfilAcesso.Profissional.ToString().ToUpperInvariant()))
        {
            especialista = await _especialistaRepository.GetByEmailAsync(request.Email);
            NotFoundException.ThrowIfNull(especialista, $"Especialista com o e-mail '{request.Email}' não encontrado");
            response = new AuthenticatedUserResponse(new Credentials(usuarioAutenticado.Token), profissional: EspecialistaResponse.MapFromEntity(especialista));
        }
        else
            response = new AuthenticatedUserResponse(new Credentials(usuarioAutenticado.Token), usuario: usuarioAutenticado);

        return await Task.FromResult(response);
    }
}