using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.DeleteAuthenticatedUserInfo;

public class DeleteAuthenticatedUserInfoRequestHandler : IRequestHandler<DeleteAuthenticatedUserInfoRequest, Result<bool>>
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;
    private readonly IAccountManagerService _accountManagerService;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IEspecialistaRepository _especialistaRepository;

    public DeleteAuthenticatedUserInfoRequestHandler(
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

    public async Task<Result<bool>> Handle(DeleteAuthenticatedUserInfoRequest request, CancellationToken cancellationToken)
    {
        var authenticatedUser = _httpContext.HttpContext.User;
        UnauthorizedRequestException.ThrowIfNotAuthenticated(authenticatedUser.Identity!.IsAuthenticated, "Usuário não está autenticado na plataforma");

        var user = await _accountManagerService.GetUserAsync(authenticatedUser);
        NotFoundException.ThrowIfNull(user, "Usuário não está autenticado na plataforma");

        var isPaciente = authenticatedUser.IsInRole(ETipoPerfilAcesso.Paciente.ToString());
        var isProfissional = authenticatedUser.IsInRole(ETipoPerfilAcesso.Profissional.ToString());

        var result = await _accountManagerService.DeactivateUserAccountAsync(user);

        if (result)
        {
            var @event = new DesativarContaCreatedEvent();
            if (isPaciente)
                @event.Paciente = await _pacienteRepository.GetByUserIdAsync(user.Id);
            if (isProfissional)
                @event.Especialista = await _especialistaRepository.GetByUserIdAsync(user.Id);

            _eventBus.Publish<DesativarContaCreatedEvent>(@event, _configuration.GetSection("RabbitMQ:DeactivateAccountQueueName").Value);
        }

        return result
            ? Result.Success(true)
            : Result.Error<bool>(new OperationErrorException("Falha ao inativar o usuário."));
    }
}