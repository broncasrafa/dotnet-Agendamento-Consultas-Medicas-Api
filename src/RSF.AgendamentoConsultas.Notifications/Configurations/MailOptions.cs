namespace RSF.AgendamentoConsultas.Notifications.Configurations;

public class MailOptions
{
    public string From { get; set; }
    public string User { get; set; }
    public int Port { get; set; }
    public string Host { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
}