using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;


namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ChangePassword;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(c => c.NewPassword).Cascade(CascadeMode.Stop)
            .PasswordValidations();

        RuleFor(c => c.OldPassword).Cascade(CascadeMode.Stop)
            .NotNullOrEmptyValidators("senha antiga");
    }
}