using RSF.AgendamentoConsultas.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

//builder.Logging.AddConsole(); // Adiciona logs no console
//builder.Logging.AddDebug();

var app = builder.Build();

app.Configure();

await app.RunAsync();
