using Slugify;


namespace RSF.AgendamentoConsultas.Domain.Utils;

public static class DomainUtil
{
    /// <summary>
    /// Gera uma string em formato "slug", para URL mais legíveis e SEO-friendly
    /// </summary>
    /// <param name="value">valor a ser slugify</param>
    /// <returns>uma string slugfied</returns>
    public static string GenerateSlugifyString(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;

        var slugHelper = new SlugHelper();
        var slugifiedString = slugHelper.GenerateSlug(value);
        return slugifiedString;
    }
}