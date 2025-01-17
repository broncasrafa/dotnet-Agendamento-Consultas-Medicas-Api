﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

namespace RSF.AgendamentoConsultas.IoC;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionConsumers
{
    public static IServiceCollection RegisterConsumersServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<PerguntaCreatedSubscriber>();
        services.AddHostedService<RespostaCreatedSubscriber>();
        services.AddHostedService<AgendamentoConsultaCreatedSubscriber>();
        
        return services;
    }
}