using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmail;

public class ConfirmEmailRequestHandler : IRequestHandler<ConfirmEmailRequest, Result<bool>>
{
    private readonly IAccountManagerService _accountManagerService;

    public ConfirmEmailRequestHandler(IAccountManagerService accountManagerService) => _accountManagerService = accountManagerService;

    public async Task<Result<bool>> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _accountManagerService.ConfirmEmailAsync(request.UserId, request.Code);

        return await Task.FromResult(result);
    }
}