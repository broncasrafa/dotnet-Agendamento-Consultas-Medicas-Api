using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.Domain.Notifications;
using RSF.AgendamentoConsultas.Notifications.Configurations;

namespace RSF.AgendamentoConsultas.Notifications;

public sealed class MailSender : IMailSender
{
    private readonly ILogger<MailSender> _logger;
    private readonly IOptions<MailOptions> _options;

    public MailSender(ILogger<MailSender> logger, IOptions<MailOptions> options)
    {
        _logger = logger;
        _options = options;
    }


    public async Task SendMailAsync(MailTo to, string subject, string body)
    {
        var host = _options.Value.Host;
        var port = _options.Value.Port;
        var password = _options.Value.Password;
        var user = _options.Value.User;
        var displayName = _options.Value.DisplayName;

        using var smtpClient = new SmtpClient(host, port);
        smtpClient.EnableSsl = true;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(user, password);

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(user, displayName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(new MailAddress(to.email, to.Name));

        _logger.LogInformation("Sending e-mail");

        await smtpClient.SendMailAsync(mailMessage);
    }
}
