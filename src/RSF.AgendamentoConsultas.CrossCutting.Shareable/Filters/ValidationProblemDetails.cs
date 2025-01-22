using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RSF.AgendamentoConsultas.CrossCutting.Shareable.Filters;

public class ValidationProblemDetails : ProblemDetails
{
    [JsonProperty(Order = 4)]
    public ICollection<string> Errors { get; set; } = new List<string>();
}