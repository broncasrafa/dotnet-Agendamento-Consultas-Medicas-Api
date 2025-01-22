using RSF.AgendamentoConsultas.CrossCutting.Shareable.Validations.Validators;
using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Account.Command.LoginConsultor;

public class LoginConsultorRequestValidator : AbstractValidator<LoginConsultorRequest>
{
    public LoginConsultorRequestValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .EmailValidations();

        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Senha é obrigatório, não deve ser nulo ou vazia");
    }
}