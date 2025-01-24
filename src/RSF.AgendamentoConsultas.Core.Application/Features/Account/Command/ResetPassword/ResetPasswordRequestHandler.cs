using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ResetPassword;

public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, Result<bool>>
{
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;
    private readonly IAccountManagerService _accountManagerService;

    public ResetPasswordRequestHandler(IEventBus eventBus, IConfiguration configuration, IAccountManagerService accountManagerService)
    {
        _eventBus = eventBus;
        _configuration = configuration;
        _accountManagerService = accountManagerService;
    }

    public async Task<Result<bool>> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _accountManagerService.FindByEmailAsync(request.Email);
        NotFoundException.ThrowIfNull(user, $"Usuário com o e-mail '{request.Email}' não encontrado");

        var result = await _accountManagerService.ResetPasswordAsync(user, request.ResetCode, request.NewPassword);

        if (result)
        {
            var @event = new ChangePasswordCreatedEvent(usuario: UsuarioAutenticadoModel.MapFromEntity(user));
            _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:ChangePasswordQueueName").Value);
        }

        return await Task.FromResult(result);
    }
}