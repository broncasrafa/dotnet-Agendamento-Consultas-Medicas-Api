namespace RSF.AgendamentoConsultas.MessageBroker.Configurations;

public class RabbitMQSettings
{
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string VirtualHost { get; set; }
    public int Port { get; set; }

    
    public string PerguntasQueueName { get; set; }
    public string RespostasQueueName { get; set; }    
    public string AgendamentoQueueName { get; set; }    
}