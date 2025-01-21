using RSF.AgendamentoConsultas.Workers.AgendamentosExpirados.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterWorkersServices(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();

app.UseHttpsRedirection();

app.ConfigureHangfire();

app.Run();