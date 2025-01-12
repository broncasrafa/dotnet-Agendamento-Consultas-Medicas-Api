namespace RSF.AgendamentoConsultas.MessageBroker.Configurations;

public class RabbitMQOptions
{
    public string ConnectionString { get; set; }
    public string ServerName { get; set; }
    public IEnumerable<string>? HostNames { get; set; }
    public string ClientProviderName { get; set; }
}