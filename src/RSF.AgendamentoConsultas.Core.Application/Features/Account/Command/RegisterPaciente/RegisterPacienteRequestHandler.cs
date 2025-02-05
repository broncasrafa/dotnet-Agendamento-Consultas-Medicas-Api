using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterPaciente;

public class RegisterPacienteRequestHandler : IRequestHandler<RegisterPacienteRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;

    public RegisterPacienteRequestHandler(
        IAccountManagerService accountManagerService,
        IPacienteRepository pacienteRepository,
        IEventBus eventBus,
        IConfiguration configuration)
    {
        _accountManagerService = accountManagerService;
        _pacienteRepository = pacienteRepository;
        _eventBus = eventBus;
        _configuration = configuration;
    }

    public async Task<Result<AuthenticatedUserResponse>> Handle(RegisterPacienteRequest request, CancellationToken cancellationToken)
    {
        var isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(c => c.Documento == request.CPF);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(c => c.Email == request.Email);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        var newUser = await _accountManagerService.RegisterAsync(request.NomeCompleto, request.CPF, request.Username, request.Email, request.Telefone, request.Genero, request.Password, ETipoPerfilAcesso.Paciente);

        if (newUser is not null)
        {
            var newPaciente = new Domain.Entities.Paciente(
            userId: newUser.Id,
            nome: request.NomeCompleto,
            cpf: request.CPF,
            email: request.Email,
            telefone: request.Telefone,
            genero: request.Genero,
            dataNascimento: request.DataNascimento.ToString("yyyy-MM-dd"),
            termoUsoAceito: true);

            await _pacienteRepository.AddAsync(newPaciente);

            var code = await _accountManagerService.GetEmailConfirmationTokenAsync(request.Email);

            if (!string.IsNullOrWhiteSpace(code))
            {
                var @event = new EmailConfirmationCreatedEvent(usuario: newUser, code);
                _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:EmailConfirmationQueueName").Value);
            }

            var response = new AuthenticatedUserResponse(usuario: newUser);
            return Result.Success(response);
        }

        return Result.Error<AuthenticatedUserResponse>(new OperationErrorException("Falha ao realizar o registro do novo usuário."));
    }
}
