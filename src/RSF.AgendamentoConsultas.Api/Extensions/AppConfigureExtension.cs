using RSF.AgendamentoConsultas.Api.Endpoints;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RSF.AgendamentoConsultas.Api.Extensions;

internal static class AppConfigureExtension
{
    public static void Configure(this WebApplication app)
    {
        app.ConfigureSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHealthChecks("/health");
        app.UseStaticFiles();
        app.UseExceptionHandler();
        app.UseCors("AllowAllPolicy");
        app.MapEndpoints();
    }


    private static void ConfigureSwaggerUI(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Agendamento de Consultas Médicas Api");
                c.DocExpansion(DocExpansion.None);
                c.DisplayRequestDuration();
                c.ShowExtensions();
                c.ShowCommonExtensions();
                c.EnableDeepLinking();

                //c.InjectStylesheet("/css/swagger-ui/swagger-dark.css");
            });
        }
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        app.MapRegiaoEndpoints();
        app.MapEstadoEndpoints();
        app.MapCidadeEndpoints();
        
        return app;
    }
}