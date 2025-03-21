namespace RSF.AgendamentoConsultas.Core.Domain.Notifications;

#pragma warning disable CA1002
public interface IMailSender
{
    Task SendMailAsync(MailTo to,  string subject, string body);
    Task SendMailAsync(List<MailTo> toList, string subject, string body, List<MailTo>? ccList = null, List<MailTo>? bccList = null);
}
#pragma warning restore CA1002