using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Validators;

public static class IdValidator
{
    public static IRuleBuilderOptions<T, int> IdValidators<T>(this IRuleBuilder<T, int> ruleBuilder, string fieldName)
        => ruleBuilder.GreaterThan(0).WithMessage($"O ID do(a) {fieldName} deve ser maior que 0.");
    public static IRuleBuilderOptions<T, int?> IdValidators<T>(this IRuleBuilder<T, int?> ruleBuilder, string fieldName, Func<T, bool> predicate)
        => ruleBuilder.GreaterThan(0).When(predicate).WithMessage($"O ID do(a) {fieldName} deve ser maior que 0.");
    public static IRuleBuilderOptions<T, int> IdValidators<T>(this IRuleBuilder<T, int> ruleBuilder, string fieldName, Func<T, bool> predicate)
        => ruleBuilder.GreaterThan(0).When(predicate).WithMessage($"O ID do(a) {fieldName} deve ser maior que 0.");

    public static IRuleBuilderOptions<T, Guid> IdValidators<T>(this IRuleBuilder<T, Guid> ruleBuilder, string field)
        => ruleBuilder
            .Must(guid => guid != Guid.Empty)
            .WithMessage($"O ID {field} informado é inválido.");
}