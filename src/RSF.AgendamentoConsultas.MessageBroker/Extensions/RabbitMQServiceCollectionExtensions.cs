using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;


namespace RSF.AgendamentoConsultas.MessageBroker.Extensions;

[ExcludeFromCodeCoverage]
public static class RabbitMQServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQOptions>(options => configuration.GetSection("RabbitMQ").Bind(options));
        services.Configure<ProducerOptions>(options => configuration.GetSection("RabbitMQ:Producer").Bind(options));
        services.Configure<ConsumerOptions>(options => configuration.GetSection("RabbitMQ:Consumer").Bind(options));

        if (configuration.GetSection("RabbitMQ:MessageConfirmation").Exists())
        {
            services.Configure<MessageConfirmationOptions>(options => configuration.GetSection("RabbitMQ:MessageConfirmation").Bind(options));
        }
        else
        {
            services.Configure<MessageConfirmationOptions>(options => options.SaveMessagesToFile = false);
        }

        services.AddSingleton(CreateConnection);

        return services;
    }


    private static IConnection CreateConnection(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<IConnection>>();
        logger.LogDebug("Creating RabbitMQ Connection");

        try
        {
            var options = serviceProvider.GetRequiredService<IOptions<RabbitMQOptions>>();

            if (string.IsNullOrWhiteSpace(options.Value.ConnectionString))
                throw new InvalidOperationException("A configuração 'ConnectionString' do RabbitMQ não foi fornecida corretamente.");
            if (options.Value.HostNames is null)
                throw new InvalidOperationException("A configuração 'HostNames' do RabbitMQ não foi fornecida corretamente.");

            //var factory = new ConnectionFactory()
            //{
            //    Uri = new Uri(options.Value.ConnectionString),
            //    AutomaticRecoveryEnabled = true,
            //    DispatchConsumersAsync = true,
            //    RequestedHeartbeat = TimeSpan.FromSeconds(60)
            //};

            //return factory.CreateConnection(options.Value.HostNames.ToList(), options.Value.ClientProviderName);

            var factory = new ConnectionFactory()
            {
                HostName = options.Value.ConnectionString
            };
            return factory.CreateConnection();
        }
        catch (BrokerUnreachableException ex)
        {
            logger.LogError(ex, "Não foi possível realizar a conexão com o servidor do RabbitMQ");
            throw;
        }
    }


    public static IHealthChecksBuilder AddRabbitMQHealthCheck(this IHealthChecksBuilder healthChecksBuilder, IConfiguration configuration)
    {
        return healthChecksBuilder;
    }
}