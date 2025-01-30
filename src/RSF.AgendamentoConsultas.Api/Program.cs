using RSF.AgendamentoConsultas.Api.Extensions;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

try
{
	Log.Information("Starting web application");

	var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Assembly", typeof(Program).Assembly.GetName().Name)
            .WriteTo.Console(theme: AnsiConsoleTheme.Code,
                outputTemplate: "[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.Seq(context.Configuration["Seq:ServerUrl"]);
    });

    builder.Services.ConfigureServices(builder.Configuration);

    var app = builder.Build();
	await app.Configure();

	await app.RunAsync();
}
catch(Exception ex)
{
	Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
	await Log.CloseAndFlushAsync();
}
