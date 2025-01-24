using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Core.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories.Common;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Repositories;
using RSF.AgendamentoConsultas.Core.Domain.Interfaces.Services;
using RSF.AgendamentoConsultas.Infra.Data.Context;
using RSF.AgendamentoConsultas.Infra.Data.Repositories;
using RSF.AgendamentoConsultas.Infra.Data.Repositories.Common;
using RSF.AgendamentoConsultas.Infra.MessageBroker;
using RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications;
using RSF.AgendamentoConsultas.Infra.Notifications.Configurations;
using RSF.AgendamentoConsultas.Infra.Notifications.Templates;
using RSF.AgendamentoConsultas.Infra.Identity.Context;
using RSF.AgendamentoConsultas.Infra.Identity.Configurations;
using RSF.AgendamentoConsultas.Infra.Identity.JWT;
using RSF.AgendamentoConsultas.Infra.Identity.AccountManager;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.CrossCutting.IoC;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionInfrastructure
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddIdentity(configuration);
        services.AddRepositories();
        services.AddRabbitMQ(configuration);
        services.AddAwsS3(configuration);
        services.AddMailSender(configuration);
        services.AddJwtConfiguration(configuration);

        return services;
    }

    public static async Task SeedIdentityDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        
        var services = scope.ServiceProvider;

        try
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await AppIdentityDbContextSeed.SeedAsync(userManager, roleManager);
        }
        catch (Exception ex)
        {
            var loggerFactory = services.GetService<ILoggerFactory>();
            var logger = loggerFactory?.CreateLogger("SeedLogger");
            logger!.LogError(ex, "Ocorreu um erro ao executar o seed do banco de dados.");
        }
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

    private static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        // Configurar o contexto de banco de dados
        services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Configurar o Identity
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders();
    }

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
        services.AddTransient<ForgotPasswordEmail>();
        services.AddTransient<ResetPasswordEmail>();
        services.AddTransient<EmailConfirmationEmail>();
    }


    private static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTSettings>(configuration.GetSection("JWT"));

        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAccountManagerService, AccountManagerService>();

        services.AddAuthentication(c =>
        {
            c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        context.HandleResponse();
                        await HandleAuthError(context.HttpContext, StatusCodes.Status401Unauthorized);
                    },
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";
                        await HandleAuthError(context.HttpContext, StatusCodes.Status403Forbidden);
                    }
                };
            });
    }


    private static async Task HandleAuthError(HttpContext context, int statusCode)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Instance = context.Request.Path,
            Title = statusCode switch
            {
                StatusCodes.Status401Unauthorized => "Você não está autenticado.",
                StatusCodes.Status403Forbidden => "Acesso negado.",
                _ => "Erro de autenticação."
            },
            Detail = statusCode switch
            {
                StatusCodes.Status401Unauthorized => "Envie um token válido no header da requisição de autorização.",
                StatusCodes.Status403Forbidden => "Você não tem permissão para acessar este recurso.",
                _ => "Por favor contate o suporte para mais informações."
            }
        };

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}