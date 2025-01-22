using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Infra.MessageBroker;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Workers.AgendamentosExpirados.Jobs;
using Hangfire;
using Hangfire.Console;
using Hangfire.Redis.StackExchange;
using StackExchange.Redis;

namespace RSF.AgendamentoConsultas.Workers.AgendamentosExpirados.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionsRegister
{
    public static IServiceCollection RegisterWorkersServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureHangfire(configuration);
        services.ConfigureJobs();
        services.ConfigureExternalServices(configuration);
        return services;
    }

    public static void ConfigureHangfire(this WebApplication app)
        => app.UseHangfireDashboard("/worker/agendamentos-expirados/hangfire", new DashboardOptions
        {
            DashboardTitle = "Worker de Agendamentos de Consultas Expiradas",
            DarkModeEnabled = true
        });

    private static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(options =>
        {
            var redis = ConnectionMultiplexer.Connect(configuration.GetValue<string>("RedisConfiguration:RedisConnectionString"));
            options.UseRedisStorage(redis, options: new RedisStorageOptions { Prefix = "hangfire:" });
            options.UseConsole();
        });

        services.AddHangfireServer();
    }
    private static void ConfigureJobs(this IServiceCollection services)
    {
        services.AddHostedService<AgendamentoExpiradoPacienteJob>();
        services.AddHostedService<AgendamentoExpiradoEspecialistaJob>();
    }
    private static void ConfigureExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                                .EnableSensitiveDataLogging()
                                .EnableDetailedErrors();
        });
        services.AddScoped<IAgendamentoConsultaRepository, AgendamentoConsultaRepository>();

        services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));
        services.AddSingleton<RabbitMQConnection>();
        services.AddScoped<IEventBus, RabbitMQService>();
    }
}