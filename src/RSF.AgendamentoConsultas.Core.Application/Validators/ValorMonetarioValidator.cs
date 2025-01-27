using FluentValidation;

namespace RSF.AgendamentoConsultas.Core.Application.Validators;

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
        // Calcula corretamente as casas decimais sem arredondamento
        var decimalPart = value - Math.Truncate(value); // Obtém a parte decimal exata
        var decimalString = decimalPart.ToString("G29").TrimEnd('0'); // Remove trailing zeros
        return decimalString.Contains('.') ? decimalString.Split('.')[1].Length : 0;
    }
}
/*
Casos de Teste:
-------------------------------------------------------------
Input  | Resultado    | Motivo
-------------------------------------------------------------
null	✅ Válido	    Campo opcional.
0	    ❌ Inválido    Deve ser maior que zero.
50	    ✅ Válido	    Valor válido.
50.99	✅ Válido	    Valor válido com duas casas.
50.999	❌ Inválido    Mais de duas casas decimais.
-5	    ❌ Inválido    Valor negativo.
------------------------------------------------------------
*/