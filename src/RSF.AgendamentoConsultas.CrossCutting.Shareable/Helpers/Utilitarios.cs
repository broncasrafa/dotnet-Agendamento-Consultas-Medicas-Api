using System.Text.RegularExpressions;

using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Validations;
using Slugify;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;

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

    /// <summary>
    /// Gera uma string de data e hora formatada por extenso completa. Ex.: sexta-feira, 10 de janeiro às 08:46
    /// </summary>
    /// <param name="data">datetime a ser formatada</param>
    public static string DataFormatadaExtenso(DateTime data)
        => data.ToString("dddd, dd 'de' MMMM 'às' HH:mm", new System.Globalization.CultureInfo("pt-BR"));


    /// <summary>
    /// Converte um tipo de acesso para uma string de role
    /// </summary>
    /// <param name="tipoAcesso">valor da string do perfil de acesso</param>
    /// <returns>A string do enum <see cref="ETipoPerfilAcesso"/>ETipoPerfilAcesso</returns>
    /// <exception cref="ArgumentNullException">Tipo de acesso nulo ou vazio</exception>
    /// <exception cref="ArgumentException">Tipo de acesso inválido</exception>
    public static string ConverterTipoAcessoParaRoleString(string tipoAcesso)
    {
        if (string.IsNullOrWhiteSpace(tipoAcesso)) throw new ArgumentNullException(nameof(tipoAcesso));

        if (Enum.TryParse<ETipoPerfilAcesso>(tipoAcesso, true, out var perfilAcesso))
            return perfilAcesso.ToString();

        throw new ArgumentException($"O tipo de acesso '{tipoAcesso}' não é válido.");
    }

    /// <summary>
    /// Converte um tipo de acesso para uma string de role
    /// </summary>
    /// <param name="tipoAcesso">valor da string do perfil de acesso</param>
    /// <returns>A string do enum <see cref="ETipoPerfilAcesso"/>ETipoPerfilAcesso</returns>
    /// <exception cref="ArgumentNullException">Tipo de acesso nulo ou vazio</exception>
    /// <exception cref="ArgumentException">Tipo de acesso inválido</exception>
    public static ETipoPerfilAcesso ConverterTipoAcessoParaRoleEnum(string tipoAcesso)
    {
        if (string.IsNullOrWhiteSpace(tipoAcesso)) throw new ArgumentNullException(nameof(tipoAcesso));

        if (Enum.TryParse<ETipoPerfilAcesso>(tipoAcesso, true, out var perfilAcesso))
            return perfilAcesso;

        throw new ArgumentException($"O tipo de acesso '{tipoAcesso}' não é válido.");
    }
}