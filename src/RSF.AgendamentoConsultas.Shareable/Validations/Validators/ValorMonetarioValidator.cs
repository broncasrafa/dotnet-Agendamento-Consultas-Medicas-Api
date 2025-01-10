using FluentValidation;

namespace RSF.AgendamentoConsultas.Shareable.Validations.Validators;

public static class ValorMonetarioValidator
{
    public static IRuleBuilderOptions<T, decimal?> ValorMonetarioValidations<T>(this IRuleBuilder<T, decimal?> builder, string field)
    {
        return builder
            .Must(valor => !valor.HasValue || valor > 0)
                .WithMessage($"O {field} deve ser maior que zero.")
            .Must(valor => !valor.HasValue || DecimalPlaces(valor.Value) <= 2)
                .WithMessage($"O {field} deve ter no máximo duas casas decimais.");
    }

    private static int DecimalPlaces(decimal value)
    {
        // Calcula corretamente as casas decimais
        value = Math.Abs(value);
        var decimalPart = value - Math.Floor(value);
        return decimalPart == 0 ? 0 : decimalPart.ToString("0.00").Split('.')[1].Length;
    }
}