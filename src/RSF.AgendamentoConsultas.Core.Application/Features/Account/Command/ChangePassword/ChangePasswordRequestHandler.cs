using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ChangePassword;

public class ChangePasswordRequestHandler : IRequestHandler<ChangePasswordRequest, Result<bool>>
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;
    private readonly IAccountManagerService _accountManagerService;

    public ChangePasswordRequestHandler(
        IAccountManagerService accountManagerService, 
        IHttpContextAccessor httpContext, 
        IEventBus eventBus, 
        IConfiguration configuration)
    {
        _accountManagerService = accountManagerService;
        _httpContext = httpContext;
        _eventBus = eventBus;
        _configuration = configuration;
    }

    public async Task<Result<bool>> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var authenticatedUser = _httpContext.HttpContext.User;
        UnauthorizedRequestException.ThrowIfNull(authenticatedUser, "Usuário não está autenticado na plataforma");

        var user = await _accountManagerService.GetUserAsync(authenticatedUser);
        NotFoundException.ThrowIfNull(user, "Usuário não está autenticado na plataforma");

        var response = await _accountManagerService.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

        if (response)
        {
            var @event = new ChangePasswordCreatedEvent(usuario: UsuarioAutenticadoModel.MapFromEntity(user));
            _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:ChangePasswordQueueName").Value);
        }

        return await Task.FromResult(response);
    }
}