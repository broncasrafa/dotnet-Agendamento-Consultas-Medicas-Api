using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Domain.Models;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ForgotPassword;

public class ForgotPasswordRequestHandler : IRequestHandler<ForgotPasswordRequest, Result<bool>>
{
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;
    private readonly IAccountManagerService _accountManagerService;

    public ForgotPasswordRequestHandler(IAccountManagerService accountManagerService, IEventBus eventBus, IConfiguration configuration)
    {
        _accountManagerService = accountManagerService;
        _eventBus = eventBus;
        _configuration = configuration;
    }


    public async Task<Result<bool>> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _accountManagerService.FindByEmailAsync(request.Email);
        NotFoundException.ThrowIfNull(user, $"Usuário com o e-mail '{request.Email}' não encontrado");
        
        var resetCode = await _accountManagerService.ForgotPasswordAsync(user);

        if (!string.IsNullOrWhiteSpace(resetCode))
        {
            var @event = new ForgotPasswordCreatedEvent(usuario: UsuarioAutenticadoModel.MapFromEntity(user), resetCode);
            _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:ForgotPasswordQueueName").Value);
        }

        return await Task.FromResult(!string.IsNullOrWhiteSpace(resetCode));
    }
}