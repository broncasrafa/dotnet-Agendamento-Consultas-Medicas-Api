using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Validators;

public static class EmailValidator
{
    public static IRuleBuilderOptions<T, string> EmailValidations<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
                .NotNull().WithMessage("E-mail não deve ser nulo")
                .NotEmpty().WithMessage("E-mail é obrigatório")
                .MaximumLength(200).WithMessage("E-mail não deve exceder 200 caracteres")
                .EmailAddress();
    }
}
