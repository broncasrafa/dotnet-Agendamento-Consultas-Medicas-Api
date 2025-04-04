﻿namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Validations;

internal sealed class ValidateCpf
{
    /// <summary>
    /// Verifica se o cpf é válido
    /// </summary>
    /// <param name="value">cpf</param>
    /// <returns>true se for um cpf valido</returns>
    public static bool IsValidCpf(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;

        value = value.Trim();
        value = value.Replace(".", "").Replace("-", "");

        if (value.Length != 11)
            return false;

        // Verificar se todos os dígitos são iguais
        if (value.All(c => c == value[0]))
            return false;

        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;

        tempCpf = value.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return value.EndsWith(digito);
    }

}