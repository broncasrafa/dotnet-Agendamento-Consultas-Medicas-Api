using System.Text.Json;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Extensions;

public static class StringExtensions
{
    public static string ToJson(this Object obj, bool isIndented = true) 
        => JsonSerializer.Serialize(obj, new JsonSerializerOptions() { WriteIndented = isIndented });
}