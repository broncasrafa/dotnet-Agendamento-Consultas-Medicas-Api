using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

public class ResetPasswordEmail(IMailSender mailSender, IAmazonS3 s3Client) : MailTemplateBase(mailSender, s3Client)
{
    public async Task SendEmailAsync(MailTo to, string nomeUsuario)
    {
        var htmlBody = await GetHtmlTemplateFromS3Async(key: "reseted_password_template.html");
        htmlBody = htmlBody
            .Replace("{{NomeUsuario}}", nomeUsuario);

        await SendEmailAsync(to, "Alteração de senha!", htmlBody);
    }
}