﻿using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;
using RabbitMQ.Client;

namespace RSF.AgendamentoConsultas.MessageBroker;

public sealed class RabbitMQConnection : IDisposable
{
    private bool _disposed;
    private readonly IConnection _connection;
    private readonly ILogger<RabbitMQConnection> _logger;

    public RabbitMQConnection(IOptions<RabbitMQSettings> options, ILogger<RabbitMQConnection> logger)
    {
        _logger = logger;

        _logger.LogInformation($"[{DateTime.Now}] Creating RabbitMQ Connection");

        if (string.IsNullOrWhiteSpace(options.Value.Host))
            throw new InvalidOperationException("A configuração 'Host' do RabbitMQ não foi fornecida corretamente.");

        var factory = new ConnectionFactory
        {
            HostName = options.Value.Host,
            UserName = options.Value.UserName,
            Password = options.Value.Password,
            VirtualHost = options.Value.VirtualHost,
            Port = options.Value.Port
        };

        _connection = factory.CreateConnection("rabbitmq-client-consumer");
    }

    public IModel CreateChannel()
    {
        if (_connection == null || !_connection.IsOpen)
        {
            _logger.LogError("Conexão com RabbitMQ está fechada.");
            throw new InvalidOperationException("Conexão com RabbitMQ está fechada.");
        }

        return _connection.CreateModel();
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _connection.Dispose();
    }
}