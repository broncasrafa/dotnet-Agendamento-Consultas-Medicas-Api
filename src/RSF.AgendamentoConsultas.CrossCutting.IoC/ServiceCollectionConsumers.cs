using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

namespace RSF.AgendamentoConsultas.CrossCutting.IoC;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionConsumers
{
    public static IServiceCollection RegisterConsumersServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<PerguntaCreatedSubscriber>();
        services.AddHostedService<RespostaCreatedSubscriber>();
        services.AddHostedService<AgendamentoCreatedSubscriber>();
        services.AddHostedService<AgendamentoCanceledByPacienteSubscriber>();
        services.AddHostedService<AgendamentoCanceledByEspecialistaSubscriber>();
        services.AddHostedService<AgendamentoExpiredByPacienteSubscriber>();
        services.AddHostedService<AgendamentoExpiredByEspecialistaSubscriber>();
        services.AddHostedService<ForgotPasswordSubscriber>();
        
        return services;
    }
}