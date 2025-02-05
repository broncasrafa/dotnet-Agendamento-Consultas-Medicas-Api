using Microsoft.Extensions.Configuration;
using RSF.AgendamentoConsultas.Core.Application.Features.Account.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.Register;

public class RegisterRequestHandler: IRequestHandler<RegisterRequest, Result<AuthenticatedUserResponse>>
{
    private readonly IAccountManagerService _accountManagerService;
    private readonly IEventBus _eventBus;
    private readonly IConfiguration _configuration;

    public RegisterRequestHandler(IAccountManagerService accountManagerService, IEventBus eventBus, IConfiguration configuration)
    {
        _accountManagerService = accountManagerService;
        _eventBus = eventBus;
        _configuration = configuration;
    }

    public async Task<Result<AuthenticatedUserResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var isUserAlreadyExists = await _accountManagerService.CheckIfAlreadyExistsByFilterAsync(c => c.Documento == request.CPF);
        AlreadyExistsException.ThrowIfExists(isUserAlreadyExists, "Usuário já cadastrado");

        var perfil = Utilitarios.ConverterTipoAcessoParaRoleEnum(request.TipoAcesso);

        var result = await _accountManagerService.RegisterAsync(request.NomeCompleto, request.CPF, request.Username, request.Email, request.Telefone, request.Genero, request.Password, perfil);

        var code = await _accountManagerService.GetEmailConfirmationTokenAsync(request.Email);

        if (!string.IsNullOrWhiteSpace(code))
        {
            var @event = new EmailConfirmationCreatedEvent(usuario: result, code);
            _eventBus.Publish(@event, _configuration.GetSection("RabbitMQ:EmailConfirmationQueueName").Value);
        }

        var response = new AuthenticatedUserResponse(usuario: result);

        //return await Task.FromResult(response);
        return response.Usuario is not null
            ? Result.Success(response)
            : Result.Error<AuthenticatedUserResponse>(new OperationErrorException("Falha ao realizar o registro do novo usuário."));
    }
}