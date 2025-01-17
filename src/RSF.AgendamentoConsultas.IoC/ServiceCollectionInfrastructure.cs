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
using RSF.AgendamentoConsultas.Domain.MessageBus;
using RSF.AgendamentoConsultas.Domain.Notifications;
using RSF.AgendamentoConsultas.MessageBroker;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Notifications;
using RSF.AgendamentoConsultas.Notifications.Configurations;
using RSF.AgendamentoConsultas.Notifications.Templates;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.IoC;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionInfrastructure
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddRabbitMQ(configuration);
        services.AddAwsS3(configuration);
        services.AddMailSender(configuration);

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
        services.AddScoped<IPerguntaRepository, PerguntaRepository>();
        services.AddScoped<IPerguntaRespostaRepository, PerguntaRespostaRepository>();
        services.AddScoped<IPerguntaRespostaReacaoRepository, PerguntaRespostaReacaoRepository>();
    }

    private static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQ"));
        //services.Configure<ProducerOptions>(configuration.GetSection("RabbitMQ:Producer"));
        //services.Configure<ConsumerOptions>(configuration.GetSection("RabbitMQ:Consumer"));
        ////services.Configure<RabbitMQOptions>(options => configuration.GetSection("RabbitMQ").Bind(options));
        ////services.Configure<ProducerOptions>(options => configuration.GetSection("RabbitMQ:Producer").Bind(options));
        ////services.Configure<ConsumerOptions>(options => configuration.GetSection("RabbitMQ:Consumer").Bind(options));

        //if (configuration.GetSection("RabbitMQ:MessageConfirmation").Exists())
        //{
        //    services.Configure<MessageConfirmationOptions>(options => configuration.GetSection("RabbitMQ:MessageConfirmation").Bind(options));
        //}
        //else
        //{
        //    services.Configure<MessageConfirmationOptions>(options => options.SaveMessagesToFile = false);
        //}

        //services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
        //{
        //    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
        //    var options = sp.GetRequiredService<IOptions<RabbitMQOptions>>();
        //    return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory, options);
        //});


        services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQ"));
        services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));

        services.AddScoped<IRabbitMQService, RabbitMQService>();

        //services.AddSingleton<RabbitMQConnection>();
        //services.AddSingleton<IEventBus, RabbitMQBus>();
    }
    
    private static void AddAwsS3(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        services.AddAWSService<IAmazonS3>();
    }

    private static void AddMailSender(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailOptions>(configuration.GetSection("MailSettings"));

        services.AddTransient<IMailSender, MailSender>();

        // Registrando e-mails específicos
        services.AddTransient<PerguntaCreatedEmail>();
        services.AddTransient<RespostaCreatedEmail>();
    }
    
    
}