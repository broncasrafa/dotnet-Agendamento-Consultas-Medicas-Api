using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RSF.AgendamentoConsultas.Api.Configurations.Swagger;

public class OrderEndpointsByHTTPVerb : IDocumentFilter
{
    private static readonly string[] stringArray = new[] { "get", "post", "put", "delete", "patch", "options", "head" };

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var path in swaggerDoc.Paths)
        {
            // Ordena os verbos HTTP de cada caminho
            var verbsOrder = new[] { "get", "post", "put", "delete", "patch", "options", "head" };
            var sortedOperations = path.Value.Operations
                .OrderBy(op => Array.IndexOf(verbsOrder, op.Key.ToString().ToLower()))
                .ToDictionary(op => op.Key, op => op.Value);

            // Atualiza a ordem das operações
            path.Value.Operations = sortedOperations;
        }
    }
}