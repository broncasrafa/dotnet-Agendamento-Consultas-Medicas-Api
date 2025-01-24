using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Exceptions;
using MediatR;
using OperationResult;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmail;

/// <summary>
/// Handler para confirmar o e-mail do usuário
/// </summary>
public class ConfirmEmailRequestHandler : IRequestHandler<ConfirmEmailRequest, Result<bool>>
{
    private readonly IAccountManagerService _accountManagerService;
    private readonly IPacienteRepository _pacienteRepository;

    public ConfirmEmailRequestHandler(IAccountManagerService accountManagerService, IPacienteRepository pacienteRepository)
    {
        _accountManagerService = accountManagerService;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Result<bool>> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var result = await _accountManagerService.ConfirmEmailAsync(request.UserId, request.Code);

        if (result)
        {
            #region [ atualiza o email verificado do paciente ]
            var paciente = await _pacienteRepository.GetByFilterAsync(c => c.UserId == request.UserId);
            if (paciente is not null)
            {
                paciente.EmailVerificado = result;
                await _pacienteRepository.UpdateAsync(paciente);
            }
            #endregion
        }

        return result
           ? Result.Success(true)
           : Result.Error<bool>(new OperationErrorException("Falha ao confirmar o e-mail do usuário."));
    }
}