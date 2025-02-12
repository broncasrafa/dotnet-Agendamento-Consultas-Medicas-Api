using System.Globalization;
using System.Text.RegularExpressions;

using FluentValidation;

using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;

namespace RSF.AgendamentoConsultas.Core.Application.Validators;

public static class ValorMonetarioValidator
{
    public static IRuleBuilderOptions<T, decimal?> ValorMonetarioValidations<T>(this IRuleBuilder<T, decimal?> builder, string field)
    {
        return builder
            .Must(valor => !valor.HasValue || valor > 0)
                .WithMessage($"O {field} deve ser maior que zero.")
            .Must(valor => !valor.HasValue || IsValidDecimalFormat(valor.Value))
                .WithMessage($"O {field} deve ter no máximo duas casas decimais.");
    }

    private static bool IsValidDecimalFormat(decimal value)
    {
        return Utilitarios.IsCurrencyValid(value, 2, false);
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