using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using RSF.AgendamentoConsultas.MessageBroker.Configurations;
using RabbitMQ.Client;

namespace RSF.AgendamentoConsultas.MessageBroker;

public sealed class RabbitMQConnection : IDisposable
{
    private bool _disposed;
    private readonly IConnection _connection;
    private readonly ILogger<RabbitMQConnection> _logger;

    public RabbitMQConnection(IOptions<RabbitMQOptions> options, ILogger<RabbitMQConnection> logger)
    {
        _logger = logger;

        _logger.LogDebug("Creating RabbitMQ Connection");

        if (string.IsNullOrWhiteSpace(options.Value.ConnectionString))
            throw new InvalidOperationException("A configuração 'ConnectionString' do RabbitMQ não foi fornecida corretamente.");

        var factory = new ConnectionFactory
        {
            HostName = options.Value.ConnectionString
        };

        _connection = factory.CreateConnection();
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