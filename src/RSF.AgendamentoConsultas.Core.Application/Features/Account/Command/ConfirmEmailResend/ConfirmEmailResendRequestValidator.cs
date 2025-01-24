using RSF.AgendamentoConsultas.CrossCutting.Shareable.Validations.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ConfirmEmailResend;

public class ConfirmEmailResendRequestValidator : AbstractValidator<ConfirmEmailResendRequest>
{
    public ConfirmEmailResendRequestValidator()
    {
        RuleFor(c => c.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();
    }
}