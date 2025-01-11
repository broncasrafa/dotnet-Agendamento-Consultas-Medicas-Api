using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Shareable;
using RSF.AgendamentoConsultas.Application;
using RSF.AgendamentoConsultas.Application.Services.HasherPassword;
using MediatR;
using FluentValidation;
using AutoMapper;
using RSF.AgendamentoConsultas.Application.Behaviours.Pipelines;

namespace RSF.AgendamentoConsultas.IoC;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionCoreApplication
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureMediatR(new[] { typeof(ApplicationEntryPoint).Assembly });
        services.ConfigureFluentValidation(typeof(ShareableEntryPoint).Assembly, typeof(ApplicationEntryPoint).Assembly);
        services.ConfigureAutoMapper(new[] { typeof(ApplicationEntryPoint).Assembly });

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }

    

    private static void ConfigureMediatR(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddMediatR(assemblies);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionPipeline<,>));
    }

    private static void ConfigureFluentValidation(this IServiceCollection services, params Assembly[] assemblies)
    {
        Type abstractValidatorType = typeof(AbstractValidator<>);
        object[] objArray = (from type in assemblies.SelectMany((Assembly a) => a.DefinedTypes)
                             where (type.BaseType?.IsGenericType ?? false) && type.BaseType.GetGenericTypeDefinition() == abstractValidatorType
                             select type).Select(Activator.CreateInstance).ToArray()!;

        foreach (object obj in objArray)
            services.AddSingleton(obj.GetType().BaseType!, obj);
    }

    private static void ConfigureAutoMapper(this IServiceCollection services, Assembly[] assemblies)
    {
        var profileType = typeof(Profile);
        var profiles = assemblies
                        .SelectMany(a => a.DefinedTypes)
                        .Where(type => profileType.IsAssignableFrom(type))
                        .Select(Activator.CreateInstance)
                        .Cast<Profile>()
                        .ToArray();

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            foreach (var profile in profiles)
                cfg.AddProfile(profile);
        });

        mapperConfiguration.AssertConfigurationIsValid();

        var mapper = mapperConfiguration.CreateMapper();

        services.AddSingleton(mapper);
    }
}