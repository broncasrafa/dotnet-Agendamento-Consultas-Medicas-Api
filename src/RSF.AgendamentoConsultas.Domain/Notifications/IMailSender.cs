namespace RSF.AgendamentoConsultas.Domain.Notifications;

public interface IMailSender
{
    Task SendMailAsync(MailTo to,  string subject, string body);
}