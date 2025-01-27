using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using RSF.AgendamentoConsultas.Api.Middlewares;
using RSF.AgendamentoConsultas.CrossCutting.IoC;
using RSF.AgendamentoConsultas.CrossCutting.Shareable;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Api.Extensions;

internal static class ConfigureServicesExtension
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddControllerAndJsonConfig();

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddEndpointsApiExplorer();        

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        services.AddSwaggerGen();

        services.AddCorsConfig();

        services.AddHealthChecks();

        services.AddHttpContextAccessor();

        services.AddAuthorizationAndPolices();

        services.AddValidatorsFromAssemblyContaining<ShareableEntryPoint>();

        services.RegisterInfrastructureServices(configuration);
        
        services.RegisterApplicationServices(configuration);

        services.RegisterConsumersServices(configuration);
    }



    private static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Agendamento de Consultas Médicas Api",
                Version = "v1",
                Description = "Demonstração dos recursos disponíveis na api",
                Contact = new OpenApiContact
                {
                    Name = "Rafael Francisco",
                    Email = "rsfrancisco.applications@gmail.com",
                    Url = new Uri("https://github.com/broncasrafa")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Informe seu Token Bearer para acessar os recursos da API da seguinte forma: Bearer {your token here}",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    }, new List<string>()
                },
            });


            //Set the comments path for the Swagger JSON and UI.
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
    }

    private static void AddControllerAndJsonConfig(this IServiceCollection services)
    {
        services
            .AddControllers()
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            //});
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
    }

    private static void AddCorsConfig(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllPolicy", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
        });
    }

    private static void AddAuthorizationAndPolices(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build())
            .AddPolicy("AdminOrEspecialista", policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(ETipoPerfilAcesso.Administrador.ToString()) ||
                    context.User.IsInRole(ETipoPerfilAcesso.Profissional.ToString())
                ))
            .AddPolicy("AdminOrPaciente", policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(ETipoPerfilAcesso.Administrador.ToString()) ||
                    context.User.IsInRole(ETipoPerfilAcesso.Paciente.ToString())
                ))
            .AddPolicy("AdminOrConsultor", policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(ETipoPerfilAcesso.Administrador.ToString()) ||
                    context.User.IsInRole(ETipoPerfilAcesso.Consultor.ToString())
                ))
            .AddPolicy("AdminOrPacienteOrEspecialista", policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(ETipoPerfilAcesso.Administrador.ToString()) ||
                    context.User.IsInRole(ETipoPerfilAcesso.Paciente.ToString()) ||
                    context.User.IsInRole(ETipoPerfilAcesso.Profissional.ToString())
                ))
            .AddPolicy("PacienteOrEspecialista", policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole(ETipoPerfilAcesso.Paciente.ToString()) ||
                    context.User.IsInRole(ETipoPerfilAcesso.Profissional.ToString())
                ))
            .AddPolicy("OnlyEspecialistas", policy =>
                policy.RequireRole(ETipoPerfilAcesso.Profissional.ToString()))
            .AddPolicy("OnlyPacientes", policy =>
                policy.RequireRole(ETipoPerfilAcesso.Paciente.ToString()))
            .AddPolicy("OnlyAdmin", policy =>
                policy.RequireRole(ETipoPerfilAcesso.Administrador.ToString()))


            ;
    }
}

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Microsoft.AspNetCore.Mvc.NewtonsoftJson