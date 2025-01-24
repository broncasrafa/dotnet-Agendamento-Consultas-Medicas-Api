using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmailResend;

/// <summary>
/// Handler para reenviar o codigo de confirmação de e-mail para o usuário
/// </summary>
public class ConfirmEmailResendRequestHandler : IRequestHandler<ConfirmEmailResendRequest, Result<bool>>
{
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;
    private readonly IAccountManagerService _accountManagerService;

    public ConfirmEmailResendRequestHandler(IEventBus eventBus, IConfiguration configuration, IAccountManagerService accountManagerService)
    {
        _eventBus = eventBus;
        _configuration = configuration;
        _accountManagerService = accountManagerService;
    }

    public async Task<Result<bool>> Handle(ConfirmEmailResendRequest request, CancellationToken cancellationToken)
    {
        var user = await _accountManagerService.FindByEmailAsync(request.Email);
        NotFoundException.ThrowIfNull(user, $"Usuário com o e-mail '{request.Email}' não encontrado");

        var code = await _accountManagerService.ResendEmailConfirmationTokenAsync(request.Email);

        if (!string.IsNullOrWhiteSpace(code))
        {
            var @event = new EmailConfirmationCreatedEvent(usuario: UsuarioAutenticadoModel.MapFromEntity(user), code);
            _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:EmailConfirmationQueueName").Value);
        }

        return await Task.FromResult(!string.IsNullOrWhiteSpace(code));
    }
}