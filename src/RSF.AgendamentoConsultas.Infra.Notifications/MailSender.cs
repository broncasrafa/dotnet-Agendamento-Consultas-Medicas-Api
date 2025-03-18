using System.Net;
using System.Net.Mail;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Infra.Notifications.Configurations;

namespace RSF.AgendamentoConsultas.Infra.Notifications;

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

        mailMessage.To.Add(new MailAddress(to.Email, to.Name));

        _logger.LogInformation("Sending e-mail to {Details}", JsonSerializer.Serialize(new { name = to.Name, email = to.Email }));

        await smtpClient.SendMailAsync(mailMessage);
    }
}
