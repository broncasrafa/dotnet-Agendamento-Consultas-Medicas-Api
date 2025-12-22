using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Validators;

public static class NotNullOrEmptyValidator
{
    public static IRuleBuilderOptions<T, string> NotNullOrEmptyValidators<T>(this IRuleBuilder<T, string> ruleBuilder, string field)
    {
        return ruleBuilder
                    .NotEmpty().WithMessage($"O {field} é obrigatório, não pode ser nulo ou vazio.");
    }
    public static IRuleBuilderOptions<T, string> NotNullOrEmptyValidators<T>(this IRuleBuilder<T, string> ruleBuilder, string field, int? minLength = null)
    {
        var rule = ruleBuilder
                        .NotEmpty().WithMessage($"O {field} é obrigatório, não pode ser nulo ou vazio.");

        if (minLength is not null)
            rule = rule
                    .MinimumLength(minLength.Value).WithMessage($"O {field} deve ter pelo menos {minLength} caracteres.");

        return rule;
    }
    public static IRuleBuilderOptions<T, string> NotNullOrEmptyValidators<T>(this IRuleBuilder<T, string> ruleBuilder, string field, int? minLength = null, int? maxLength = null)
    {
        var rule = ruleBuilder
                        .NotEmpty().WithMessage($"O {field} é obrigatório, não pode ser nulo ou vazio.");

        if (minLength is not null)
            rule = rule
                    .MinimumLength(minLength.Value).WithMessage($"O {field} deve ter pelo menos {minLength} caracteres.");

        if (maxLength is not null)
            rule = rule
                    .MaximumLength(maxLength.Value).WithMessage($"O {field} deve ter no máximo {maxLength} caracteres.");

        return rule;
    }
    public static IRuleBuilderOptions<T, string> NotNullOrEmptyValidators<T>(this IRuleBuilder<T, string> ruleBuilder, string field, int? minLength = null, int? maxLength = null, Func<T, bool>? predicate = null, string? message = null)
    {
        var rule = ruleBuilder.NotEmpty();

        if (minLength is not null)
            rule = rule
                    .MinimumLength(minLength.Value).WithMessage($"O {field} deve ter pelo menos {minLength} caracteres.");

        if (maxLength is not null)
            rule = rule
                    .MaximumLength(maxLength.Value).WithMessage($"O {field} deve ter no máximo {maxLength} caracteres.");

        if (predicate is not null)
            rule = rule.When(predicate);

        if (message is not null)
            rule = rule.WithMessage(message);
        else
            rule = rule.WithMessage($"O {field} é obrigatório, não pode ser nulo ou vazio.");

        return rule;
    }
}