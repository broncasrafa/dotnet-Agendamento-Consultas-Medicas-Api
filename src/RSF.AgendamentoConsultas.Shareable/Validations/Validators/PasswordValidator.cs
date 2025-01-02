using FluentValidation;

namespace RSF.AgendamentoConsultas.Shareable.Validations.Validators;

public static class PasswordValidator
{
    public static IRuleBuilderOptions<T, string> PasswordValidations<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 5)
    {
        return ruleBuilder
                        .NotNull().WithMessage("Senha não deve ser nulo")
                        .NotEmpty().WithMessage("Senha é obrigatório")
                        .MinimumLength(minimumLength).WithMessage($"Senha deve ter pelo menos {minimumLength} caracteres")
                        .Matches("[A-Z]").WithMessage("Senha deve ter pelo menos 1 letra maiuscula")
                        .Matches("[a-z]").WithMessage("Senha deve ter pelo menos 1 letra minuscula")
                        .Matches("[0-9]").WithMessage("Senha deve ter pelo menos 1 number")
                        .Matches("[!*@#$%^&+=]").WithMessage("Senha deve ter pelo menos 1 caracter especial"); // /^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[^\w\s]).{8,}$/
    }

    public static IRuleBuilderOptions<T, string> PasswordConfirmationValidations<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 8)
    {
        return ruleBuilder
                .NotNull().WithMessage("Senha de confirmação não deve ser nulo")
                .NotEmpty().WithMessage("Senha de confirmação é obrigatório")
                .MinimumLength(minimumLength).WithMessage($"Senha de confirmação deve ter pelo menos {minimumLength} caracteres")
                .Matches("[A-Z]").WithMessage("Senha de confirmação deve ter pelo menos 1 letra maiuscula")
                .Matches("[a-z]").WithMessage("Senha de confirmação deve ter pelo menos 1 letra minuscula")
                .Matches("[0-9]").WithMessage("Senha de confirmação deve ter pelo menos 1 number")
                .Matches("[!*@#$%^&+=]").WithMessage("Senha de confirmação deve ter pelo menos 1 caracter especial");
    }
}
