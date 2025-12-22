using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Validators;

public static class PaginationValidator
{
    public static IRuleBuilderOptions<T, int> PaginatedFieldValidators<T>(this IRuleBuilder<T, int> ruleBuilder, string fieldName)
        => ruleBuilder.GreaterThan(0).WithMessage($"{fieldName} deve ser maior que zero.");
}
