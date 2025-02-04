using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Validators;

public static class EmailValidator
{
    public static IRuleBuilderOptions<T, string> EmailValidations<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
                .NotEmpty().WithMessage("E-mail é obrigatório, não deve ser nulo ou vazio")
                .MaximumLength(200).WithMessage("E-mail não deve exceder 200 caracteres")
                .EmailAddress().WithMessage("E-mail inválido");
    }
}
