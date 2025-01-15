using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.Consumers.Notification.Handlers;
using RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;
using RSF.AgendamentoConsultas.Domain.Events;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;


namespace RSF.AgendamentoConsultas.IoC;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionConsumers
{
    public static IServiceCollection RegisterConsumersServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<PerguntaCreatedSubscriber>();

        //services.AddScoped<IEventHandler<PerguntaCreatedEvent>, PerguntaCreatedEventHandler>();


        return services;
    }
}