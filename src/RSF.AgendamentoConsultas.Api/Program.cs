using RSF.AgendamentoConsultas.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

await app.Configure();

await app.RunAsync();
