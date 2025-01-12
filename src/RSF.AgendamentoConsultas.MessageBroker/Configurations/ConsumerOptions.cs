namespace RSF.AgendamentoConsultas.MessageBroker.Configurations;

public class ConsumerOptions
{
    public string QueueName { get; set; }
    public string RetryExchangeName { get; set; }
    public string RetryRoutingKey { get; set; }
    public ushort PrefetchCount { get; set; }
    public int MaximumChannelPool { get; set; }
    public int MaxRetries { get; set; }
}