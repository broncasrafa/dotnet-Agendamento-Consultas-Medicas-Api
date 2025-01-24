using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

public class EmailConfirmationEmail(IMailSender mailSender, IAmazonS3 s3Client) : MailTemplateBase(mailSender, s3Client)
{
    public async Task SendEmailAsync(MailTo to, string nomeUsuario, string code, string encodedCode)
    {
        var htmlBody = await GetHtmlTemplateFromS3Async(key: "email_confirmation_template.html");
        htmlBody = htmlBody
            .Replace("{{NomeUsuario}}", nomeUsuario)
            .Replace("{{Code}}", code)
            .Replace("{{EncodedCode}}", encodedCode);

        await SendEmailAsync(to, "Confirmação de e-mail!", htmlBody);
    }
}    