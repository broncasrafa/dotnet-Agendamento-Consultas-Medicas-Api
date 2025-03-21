using System.ComponentModel;
using System.Text.Json;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;

public static class StringExtensions
{
    public static string ToJson(this Object obj, bool isIndented = true) 
        => JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = isIndented });

    public static string GetEnumDescription(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field is null) return enumValue.ToString();

        var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : enumValue.ToString();
    }
}