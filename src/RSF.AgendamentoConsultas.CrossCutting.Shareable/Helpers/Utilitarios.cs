using System.Globalization;
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
        => data.ToString("dddd, dd 'de' MMMM 'de' yyyy 'às' HH:mm", new System.Globalization.CultureInfo("pt-BR"));


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


    /// <summary>
    /// Converte o valor monetario do decimal para uma string formatada em moeda por extenso
    /// </summary>
    /// <param name="valor">valor monetario</param>
    public static string ConverterMoedaParaExtenso(decimal? valor)
    {
        if (valor is null) return default;

        return valor.Value.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
    }

    /// <summary>
    /// Validar se o valor da moeda é valido 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="decimalPlaces"></param>
    /// <param name="allowNegative"></param>
    /// <returns></returns>
    public static bool IsCurrencyValid(decimal value, int decimalPlaces, bool allowNegative)
    {
        string temp = value.ToString(CultureInfo.InvariantCulture);

        // Construção da regex inicial para verificar formato correto
        string reg0Str = @"^[0-9]*";
        if (decimalPlaces > 0)
        {
            reg0Str += @"\.?[0-9]{0," + decimalPlaces + "}";
        }
        else if (decimalPlaces < 0)
        {
            reg0Str += @"\.?[0-9]*";
        }

        reg0Str = allowNegative ? @"^-?" + reg0Str : @"^" + reg0Str;
        reg0Str += @"$";

        if (Regex.IsMatch(temp, reg0Str))
            return true;

        // Remove caracteres não numéricos, exceto '.' e '-' (se permitido)
        string reg1Str = @"[^0-9" + (decimalPlaces != 0 ? @"\." : "") + (allowNegative ? @"\-" : "") + "]";
        temp = Regex.Replace(temp, reg1Str, "");

        if (allowNegative)
        {
            // Remove ocorrências extras de '-' mantendo apenas uma no início
            bool hasNegative = temp.Length > 0 && temp[0] == '-';
            temp = temp.Replace("-", "");
            if (hasNegative) temp = "-" + temp;
        }

        if (decimalPlaces != 0)
        {
            // Manter apenas uma ocorrência de '.' e ajustar casas decimais
            int dotIndex = temp.IndexOf('.');
            if (dotIndex >= 0)
            {
                string integerPart = temp.Substring(0, dotIndex);
                string decimalPart = temp.Substring(dotIndex + 1);

                decimalPart = decimalPlaces > 0 && decimalPart.Length > decimalPlaces
                    ? decimalPart.Substring(0, decimalPlaces)
                    : decimalPart;

                temp = integerPart + "." + decimalPart;
            }
        }

        return false;
    }
}