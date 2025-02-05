using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Validators;

public static class PasswordValidator
{
    public static IRuleBuilderOptions<T, string> PasswordValidations<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 5)
    {
        return ruleBuilder
                        .NotEmpty().WithMessage("Senha é obrigatório, não deve ser nulo ou vazia")
                        .MinimumLength(minimumLength).WithMessage($"Senha deve ter pelo menos {minimumLength} caracteres")
                        .Matches("[A-Z]").WithMessage("Senha deve ter pelo menos 1 letra maiuscula")
                        .Matches("[a-z]").WithMessage("Senha deve ter pelo menos 1 letra minuscula")
                        .Matches("[0-9]").WithMessage("Senha deve ter pelo menos 1 número")
                        .Matches("[!*@#$%^&+=]").WithMessage("Senha deve ter pelo menos 1 caracter especial");
    }

    public static IRuleBuilderOptions<T, string> PasswordConfirmationValidations<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 5)
    {
        return ruleBuilder
                .NotEmpty().WithMessage("Senha de confirmação é obrigatório, não deve ser nulo ou vazia")
                .MinimumLength(minimumLength).WithMessage($"Senha de confirmação deve ter pelo menos {minimumLength} caracteres")
                .Matches("[A-Z]").WithMessage("Senha de confirmação deve ter pelo menos 1 letra maiuscula")
                .Matches("[a-z]").WithMessage("Senha de confirmação deve ter pelo menos 1 letra minuscula")
                .Matches("[0-9]").WithMessage("Senha de confirmação deve ter pelo menos 1 número")
                .Matches("[!*@#$%^&+=]").WithMessage("Senha de confirmação deve ter pelo menos 1 caracter especial");
    }
}
