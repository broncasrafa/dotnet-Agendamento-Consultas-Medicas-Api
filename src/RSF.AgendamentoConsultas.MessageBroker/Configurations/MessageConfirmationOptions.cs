namespace RSF.AgendamentoConsultas.MessageBroker.Configurations;

public class MessageConfirmationOptions
{
    public string MessagesSavePath { get; set; }
    public bool SaveMessagesToFile { get; set; }
    public bool SaveAckedMessages { get; set; }
    public int MaxMessagesBeforeSaving { get; set; }
    public TimeSpan MaxIdleTimeBeforeSaving { get; set; }
}