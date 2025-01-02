using System.Text.RegularExpressions;
using RSF.AgendamentoConsultas.Shareable.Validations;
using Slugify;

namespace RSF.AgendamentoConsultas.Shareable.Helpers;

public static class Utilitarios
{

    /// <summary>
    /// Informa se a string do cpf é valida
    /// </summary>
    /// <param name="cpf">string do cpf</param>
    /// <returns>true se for valido.</returns>
    public static bool IsCpfValid(string cpf) => ValidateCpf.IsValidCpf(cpf);

    /// <summary>
    /// Remove a formatação da string deixando apenas os números
    /// </summary>
    /// <param name="value">valor da string</param>
    /// <returns>string sem formatação, apenas numeros</returns>
    public static string RemoverFormatacaoSomenteNumeros(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return default;

        // Remove todos os caracteres que não sejam números
        return Regex.Replace(value, @"\D", "");
    }

    /// <summary>
    /// Gera uma string em formato "slug", para URL mais legíveis e SEO-friendly
    /// </summary>
    /// <param name="value">valor a ser slugify</param>
    /// <returns>uma string slugfied</returns>
    public static string GenerateSlugifyString(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return default;

        var slugHelper = new SlugHelper();
        var slugifiedString = slugHelper.GenerateSlug(value);
        return slugifiedString;
    }
}