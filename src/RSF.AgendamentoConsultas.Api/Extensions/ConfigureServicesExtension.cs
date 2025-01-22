using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.Json;
using RSF.AgendamentoConsultas.Api.Middlewares;
using RSF.AgendamentoConsultas.CrossCutting.IoC;
using RSF.AgendamentoConsultas.CrossCutting.Shareable;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


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
}

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Microsoft.AspNetCore.Mvc.NewtonsoftJson