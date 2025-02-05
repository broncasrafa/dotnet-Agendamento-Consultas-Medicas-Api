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

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.RegisterEspecialista;

public class RegisterEspecialistaRequestHandler: IRequestHandler<RegisterEspecialistaRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;
    private readonly IEspecialistaRepository _especialistaRepository;
    private readonly IEspecialidadeRepository _especialidadeRepository;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;

    public RegisterEspecialistaRequestHandler(
        IAccountManagerService accountManagerService,
        IEspecialistaRepository especialistaRepository,
        IEspecialidadeRepository especialidadeRepository,
        IEventBus eventBus,
        IConfiguration configuration)
    {
        _accountManagerService = accountManagerService;
        _especialistaRepository = especialistaRepository;
        _eventBus = eventBus;
        _configuration = configuration;
        _especialidadeRepository = especialidadeRepository;
    }

    public async Task<Result<AuthenticatedUserResponse>> Handle(RegisterEspecialistaRequest request, CancellationToken cancellationToken)
    {
        var especialidade = await _especialidadeRepository.GetByIdAsync(request.EspecialidadeId);
        NotFoundException.ThrowIfNull(especialidade, $"Especialidade com o ID: '{request.EspecialidadeId}' não encontrada");

        var isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(c => c.Email == request.Email);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(c => c.Documento == request.Licenca);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        var newUser = await _accountManagerService.RegisterAsync(request.NomeCompleto, request.Licenca, request.Username, request.Email, request.Telefone, request.Genero, request.Password, ETipoPerfilAcesso.Profissional);

        if (newUser is not null)
        {
            var newEspecialista = new Domain.Entities.Especialista(
            userId: newUser.Id,
            nome: request.NomeCompleto,
            licenca: request.Licenca,
            email: request.Email,
            genero: request.Genero,
            tipo: request.TipoCategoria);

            newEspecialista.AddNovaEspecialidade(especialidade, "Principal");

            await _especialistaRepository.AddAsync(newEspecialista);

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