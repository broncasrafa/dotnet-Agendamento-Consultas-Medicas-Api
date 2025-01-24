using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.UpdateAuthenticatedUserInfo;

public class UpdateAuthenticatedUserInfoRequestHandler : IRequestHandler<UpdateAuthenticatedUserInfoRequest, Result<bool>>
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;
    private readonly IAccountManagerService _accountManagerService;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEspecialistaRepository _especialistaRepository;

    public UpdateAuthenticatedUserInfoRequestHandler(
        IHttpContextAccessor httpContext, 
        IEventBus eventBus, 
        IConfiguration configuration, 
        IAccountManagerService accountManagerService, 
        IPacienteRepository pacienteRepository, 
        IEspecialistaRepository especialistaRepository)
    {
        _httpContext = httpContext;
        _eventBus = eventBus;
        _configuration = configuration;
        _accountManagerService = accountManagerService;
        _pacienteRepository = pacienteRepository;
        _especialistaRepository = especialistaRepository;
    }

    public async Task<Result<bool>> Handle(UpdateAuthenticatedUserInfoRequest request, CancellationToken cancellationToken)
    {
        var authenticatedUser = _httpContext.HttpContext.User;
        UnauthorizedRequestException.ThrowIfNull(authenticatedUser, "Usuário não está autenticado na plataforma");

        var user = await _accountManagerService.GetUserAsync(authenticatedUser);
        NotFoundException.ThrowIfNull(user, "Usuário não está autenticado na plataforma");

        var isPaciente = authenticatedUser.IsInRole(ETipoPerfilAcesso.Paciente.ToString());
        var isProfissional = authenticatedUser.IsInRole(ETipoPerfilAcesso.Profissional.ToString());

        // Valida se o e-mail precisa ser verificado
        bool isEmailChanged = false;
        if (!string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase))
        {
            isEmailChanged = true;
            var emailAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(u => u.Email == request.Email);
            AlreadyExistsException.ThrowIfExists(emailAlreadyExists, "E-mail já cadastrado na plataforma");
        }

        // Valida se o username precisa ser verificado
        if (!string.Equals(user.UserName, request.Username, StringComparison.OrdinalIgnoreCase))
        {
            var usernameAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(u => u.UserName == request.Username);
            AlreadyExistsException.ThrowIfExists(usernameAlreadyExists, "Nome de usuário já cadastrado na plataforma");
        }

        user.Update(request.NomeCompleto, request.Telefone, request.Email, request.Username, isEmailChanged);
        
        var response = await _accountManagerService.UpdateUserAsync(user);

        if (response)
        {
            #region [ publica mensagem na fila de email alterado ]
            if (isEmailChanged)
            {
                var code = await _accountManagerService.GetEmailConfirmationTokenAsync(request.Email);

                if (!string.IsNullOrWhiteSpace(code))
                {
                    var @event = new EmailConfirmationCreatedEvent(usuario: UsuarioAutenticadoModel.MapFromEntity(user), code);
                    _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:EmailConfirmationQueueName").Value);
                }
            }
            #endregion

            #region [ atualiza o email do paciente ]
            if (isPaciente)
            {
                var paciente = await _pacienteRepository.GetByFilterAsync(c => c.UserId == user.Id);
                if (paciente is not null)
                {
                    paciente.Email = request.Email;
                    await _pacienteRepository.UpdateAsync(paciente);
                }
            }
            #endregion

            #region [ atualiza o email do especialista ]
            if (isProfissional)
            {
                var especialista = await _especialistaRepository.GetByFilterAsync(c => c.UserId == user.Id);
                if (especialista is not null)
                {
                    especialista.Email = request.Email;
                    await _especialistaRepository.UpdateAsync(especialista);
                }
            }
            #endregion
        }

        return response
            ? Result.Success(true)
            : Result.Error<bool>(new OperationErrorException("Falha ao atualizar os dados do usuário."));
    }
}