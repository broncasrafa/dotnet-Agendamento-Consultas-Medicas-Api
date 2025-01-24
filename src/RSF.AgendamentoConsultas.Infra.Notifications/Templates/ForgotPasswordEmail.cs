using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

public class ForgotPasswordEmail : MailTemplateBase
{
    public ForgotPasswordEmail(IMailSender mailSender, IAmazonS3 s3Client) 
        : base(mailSender, s3Client)
    {
    }

    public async Task SendEmailAsync(MailTo to, string nomeUsuario, string emailUsuario, string resetCode)
    {
        var htmlBody = await GetHtmlTemplateFromS3Async(key: "forgot_password_template.html");
        htmlBody = htmlBody
            .Replace("{{NomeUsuario}}", nomeUsuario)
            .Replace("{{EmailUsuario}}", emailUsuario)
            .Replace("{{ResetCode}}", resetCode);

        await SendEmailAsync(to, "Redefinição de senha!", htmlBody);
    }
}