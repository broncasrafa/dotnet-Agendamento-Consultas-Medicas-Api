using Amazon.S3;
using RSF.AgendamentoConsultas.Core.Domain.Notifications;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

public class DesativarContaEmail(IMailSender mailSender, IAmazonS3 s3Client) 
    : MailTemplateBase(mailSender, s3Client)
{
    public async Task SendEmailAsync(MailTo to, string nomeUsuario)
    {
        var htmlBody = await GetHtmlTemplateFromS3Async(key: "deactivate_user_account_template.html");
        htmlBody = htmlBody
            .Replace("{{NomeUsuario}}", nomeUsuario);

        await SendEmailAsync(to, "Desativação da conta!", htmlBody);
    }
}