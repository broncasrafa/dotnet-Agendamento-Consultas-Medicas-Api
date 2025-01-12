namespace RSF.AgendamentoConsultas.MessageBroker.Configurations;

public class ProducerOptions
{
    public string PerguntasRespostasExchangeName { get; set; }
    public int MaximumChannelInPool { get; set; }
    public TimeSpan MaximumWaitTime { get; set; }
}