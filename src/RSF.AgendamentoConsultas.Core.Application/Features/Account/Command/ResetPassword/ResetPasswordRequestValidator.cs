using RSF.AgendamentoConsultas.Core.Application.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.ResetPassword;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(c => c.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.NewPassword).Cascade(CascadeMode.Stop)
            .PasswordValidations();

        RuleFor(c => c.ResetCode)
            .NotEmpty().WithMessage("O Código de Reset de senha é obrigatório, não deve ser nulo ou vazio");
    }
}