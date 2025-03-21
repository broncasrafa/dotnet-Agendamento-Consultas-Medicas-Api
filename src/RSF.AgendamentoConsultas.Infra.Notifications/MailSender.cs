using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using RSF.AgendamentoConsultas.Infra.Notifications.Configurations;
using Polly;

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

        _logger.LogInformation("Sending e-mail to {@Details}", new { to.Name, to.Email });

        try
        {
            var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        (exception, timeSpan, retryCount, context) =>
                        {
                            _logger.LogWarning(exception, "Retry {RetryCount} for sending email.", retryCount);
                        });

            await policy.ExecuteAsync(() => smtpClient.SendMailAsync(mailMessage));

            _logger.LogInformation("E-mail sent successfully to {Email}", to.Email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send e-mail to {Email}", to.Email);
            throw;
        }
        finally
        {
            mailMessage.Dispose();
        }
    }
    public async Task SendMailAsync(List<MailTo> toList, string subject, string body, List<MailTo>? ccList = null, List<MailTo>? bccList = null)
    {
        var host = _options.Value.Host;
        var port = _options.Value.Port;
        var password = _options.Value.Password;
        var user = _options.Value.User;
        var displayName = _options.Value.DisplayName;

        using var smtpClient = new SmtpClient(host, port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(user, password)
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(user, displayName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        // Adiciona os destinatários principais
        foreach (var to in toList)
            mailMessage.To.Add(new MailAddress(to.Email, to.Name));

        // Adiciona os destinatários em CC (cópia)
        if (ccList is not null)
        {
            foreach (var cc in ccList)
                mailMessage.CC.Add(new MailAddress(cc.Email, cc.Name));
        }

        // Adiciona os destinatários em BCC (cópia oculta)
        if (bccList is not null)
        {
            foreach (var bcc in bccList)
                mailMessage.Bcc.Add(new MailAddress(bcc.Email, bcc.Name));
        }

        _logger.LogInformation("Sending e-mail to {@Details}", new
        {
            To = toList.Select(t => new { t.Name, t.Email }),
            CC = ccList?.Select(cc => new { cc.Name, cc.Email }),
            BCC = bccList?.Select(bcc => new { bcc.Name, bcc.Email })
        });

        try
        {
            var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        (exception, timeSpan, retryCount, context) =>
                        {
                            _logger.LogWarning(exception, "Retry {RetryCount} for sending email.", retryCount);
                        });
            await policy.ExecuteAsync(() => smtpClient.SendMailAsync(mailMessage));

            _logger.LogInformation("E-mail sent successfully to {Email}", toList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send e-mail to {Email}", toList);
            throw;
        }
        finally
        {
            mailMessage.Dispose();
        }
    }
}