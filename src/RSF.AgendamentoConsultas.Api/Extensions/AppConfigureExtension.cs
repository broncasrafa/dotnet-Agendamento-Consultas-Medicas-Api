using RSF.AgendamentoConsultas.Api.Endpoints;
using RSF.AgendamentoConsultas.Api.Middlewares;
using RSF.AgendamentoConsultas.CrossCutting.IoC;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RSF.AgendamentoConsultas.Api.Extensions;

internal static class AppConfigureExtension
{
    public static async Task Configure(this WebApplication app)
    {
        await app.Services.SeedIdentityDatabaseAsync();

        app.ConfigureSwaggerUI();
        app.UseHttpsRedirection();
        app.UseCors("AllowAllPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHealthChecks("/health");
        app.MapHealthChecks("/health");
        app.UseStaticFiles();
        app.UseExceptionHandler();
        app.MapEndpoints();

        app.Use(async (context, next) =>
        {
            context.Items["StartTime"] = DateTime.Now;
            await next();
        });
        //app.UseSerilogRequestLogging();
        app.UseMiddleware<SerilogRequestLogger>();
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        app.MapRegiaoEndpoints();
        app.MapEstadoEndpoints();
        app.MapCidadeEndpoints();
        app.MapConvenioMedicoEndpoints();
        app.MapTagsEndpoints();
        app.MapEspecialidadesEndpoints();
        app.MapTipoConsultaEndpoints();
        app.MapTipoStatusConsultaEndpoints();
        app.MapTipoAgendamentoEndpoints();
        app.MapEspecialistaEndpoints();
        app.MapPacienteEndpoints();
        app.MapPacienteDependenteEndpoints();
        app.MapAvaliacaoEndpoints();
        app.MapAgendamentoConsultaEndpoints();
        app.MapPerguntasEndpoints();
        app.MapPerguntasRespostasEndpoints();
        app.MapPerguntasRespostasReacoesEndpoints();
        app.MapAccountEndpoints();
        app.MapSearchesEndpoints();

        return app;
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
}