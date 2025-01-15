using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RSF.AgendamentoConsultas.Consumers.Notification.Handlers;
using RSF.AgendamentoConsultas.Consumers.Notification.Subscribers;
using RSF.AgendamentoConsultas.Domain.Events;
using RSF.AgendamentoConsultas.Domain.MessageBus.Bus;
using RSF.AgendamentoConsultas.MessageBroker;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((context, logger) =>
    {
        IHostEnvironment env = context.HostingEnvironment;
        IConfigurationSection config = context.Configuration.GetSection("Logging");
        logger.AddConfiguration(config);
    })
    //.ConfigureServices((context, services) =>
    //{
    //    //services.Configure<RabbitMQOptions>(context.Configuration.GetSection("RabbitMQ"));
    //    //services.AddSingleton<RabbitMQConnection>();
    //    //services.AddSingleton<IEventBus, RabbitMQBus>();

    //    services.AddHostedService<PerguntaCreatedSubscriber>();

    //    //services.AddScoped<IEventHandler<PerguntaCreatedEvent>, PerguntaCreatedEventHandler>();
    //})
    .Build();


await host.RunAsync();
