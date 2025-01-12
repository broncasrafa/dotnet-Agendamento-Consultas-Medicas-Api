using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSF.AgendamentoConsultas.Data.Context;
using RSF.AgendamentoConsultas.Data.Repositories;
using RSF.AgendamentoConsultas.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Domain.Interfaces;
using RSF.AgendamentoConsultas.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.MessageBroker;
using MediatR;
using Microsoft.Extensions.Options;

namespace RSF.AgendamentoConsultas.IoC;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionInfrastructure
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddRabbitMQ(configuration);

        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                                    .EnableSensitiveDataLogging()
                                    .EnableDetailedErrors();
            });

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IRegiaoRepository, RegiaoRepository>();
        services.AddScoped<IEstadoRepository, EstadoRepository>();
        services.AddScoped<ICidadeRepository, CidadeRepository>();
        services.AddScoped<IConvenioMedicoRepository, ConvenioMedicoRepository>();
        services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();
        services.AddScoped<IEspecialistaRepository, EspecialistaRepository>();
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IPacienteDependenteRepository, PacienteDependenteRepository>();
        services.AddScoped<IAgendamentoConsultaRepository, AgendamentoConsultaRepository>();
    }

    private static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQOptions>(options => configuration.GetSection("RabbitMQ").Bind(options));
        services.Configure<ProducerOptions>(options => configuration.GetSection("RabbitMQ:Producer").Bind(options));
        services.Configure<ConsumerOptions>(options => configuration.GetSection("RabbitMQ:Consumer").Bind(options));

        if (configuration.GetSection("RabbitMQ:MessageConfirmation").Exists())
        {
            services.Configure<MessageConfirmationOptions>(options => configuration.GetSection("RabbitMQ:MessageConfirmation").Bind(options));
        }
        else
        {
            services.Configure<MessageConfirmationOptions>(options => options.SaveMessagesToFile = false);
        }

        services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
        {
            var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            var options = sp.GetRequiredService<IOptions<RabbitMQOptions>>();
            return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory, options);
        });
    }
}