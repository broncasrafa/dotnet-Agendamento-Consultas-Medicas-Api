using FluentValidation;
using RSF.AgendamentoConsultas.Core.Application.Validators;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmail;

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(c => c.UserId).NotNullOrEmptyValidators("usuário");
        RuleFor(c => c.Code).NotNullOrEmptyValidators("Código de confirmação");
    }
}