namespace RSF.AgendamentoConsultas.MessageBroker.Configurations;

public class RabbitMQOptions
{
    public string ConnectionString { get; set; }
    public string ServerName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ClientProviderName { get; set; }
    public IEnumerable<string>? HostNames { get; set; }
}