using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Common;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Infra.MessageBroker;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications;
using RSF.AgendamentoConsultas.Infra.Notifications.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.CrossCutting.IoC;

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
        services.AddScoped<ITipoStatusConsultaRepository, TipoStatusConsultaRepository>();
    }

    private static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQ"));

        services.AddSingleton<RabbitMQConnection>();
        services.AddScoped<IEventBus, RabbitMQService>();
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
        services.AddTransient<AgendamentoCreatedEmail>();
        services.AddTransient<AgendamentoCanceledByPacienteEmail>();
        services.AddTransient<AgendamentoCanceledByEspecialistaEmail>();
        services.AddTransient<AgendamentoExpiredByPacienteEmail>();
        services.AddTransient<AgendamentoExpiredByEspecialistaEmail>();
    }
}