namespace RSF.AgendamentoConsultas.Infra.MessageBroker.Configurations;

public class RabbitMQSettings
{
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string VirtualHost { get; set; }
    public string ClientProviderName { get; set; }
    public int Port { get; set; }

    
    public string PerguntasEspecialidadeQueueName { get; set; }
    public string PerguntasEspecialistaQueueName { get; set; }
    public string RespostasPerguntasQueueName { get; set; }    
    public string AgendamentoQueueName { get; set; }    
    public string AgendamentoCanceladoPacienteQueueName { get; set; }    
    public string AgendamentoCanceladoEspecialistaQueueName { get; set; }  
    public string AgendamentoExpiradoPacienteQueueName { get; set; }    
    public string AgendamentoExpiradoEspecialistaQueueName { get; set; }    
    public string ForgotPasswordQueueName { get; set; }    
    public string ChangePasswordQueueName { get; set; }    
    public string EmailConfirmationQueueName { get; set; }    
    public string DeactivateAccountQueueName { get; set; }    
}