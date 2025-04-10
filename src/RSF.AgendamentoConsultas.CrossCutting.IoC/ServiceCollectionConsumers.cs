﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;

namespace RSF.AgendamentoConsultas.CrossCutting.IoC;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionConsumers
{
    public static IServiceCollection RegisterConsumersServices(this IServiceCollection services)
    {
        services.AddHostedService<PerguntaEspecialidadeCreatedSubscriber>();
        services.AddHostedService<PerguntaEspecialistaCreatedSubscriber>();
        services.AddHostedService<RespostaCreatedSubscriber>();
        services.AddHostedService<AgendamentoCreatedSubscriber>();
        services.AddHostedService<AgendamentoCanceledByPacienteSubscriber>();
        services.AddHostedService<AgendamentoCanceledByEspecialistaSubscriber>();
        services.AddHostedService<AgendamentoExpiredByPacienteSubscriber>();
        services.AddHostedService<AgendamentoExpiredByEspecialistaSubscriber>();
        services.AddHostedService<ForgotPasswordSubscriber>();
        services.AddHostedService<ChangePasswordSubscriber>();
        services.AddHostedService<EmailConfirmationSubscriber>();
        services.AddHostedService<DesativarContaSubscriber>();
        
        return services;
    }
}