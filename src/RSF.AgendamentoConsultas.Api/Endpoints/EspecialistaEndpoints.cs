namespace RSF.AgendamentoConsultas.Api.Endpoints;

internal static class EspecialistaEndpoints
{
    public static IEndpointRouteBuilder MapEspecialistaEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/especialistas").WithTags("Especialistas");

        return routes;
    }
}