using RSF.AgendamentoConsultas.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Notifications.Templates;

public class RespostaCreatedEmail : MailTemplateBase
{
    public RespostaCreatedEmail(IMailSender mailSender, IAmazonS3 s3Client) 
        : base(mailSender, s3Client)
    {
    }

    public async Task SendEmailAsync(MailTo to, string pacienteNome, string especialistaNome, string especialidadeNome, int respostaId, string resposta)
    {
        var htmlBody = await GetHtmlTemplateFromS3Async(bucketName: "rsfrancisco.agendamentoconsultas.emailtemplates", key: "resposta_created_template.html");

        htmlBody = htmlBody
            .Replace("{{PacienteNome}}", pacienteNome)
            .Replace("{{EspecialistaNome}}", especialistaNome)            
            .Replace("{{EspecialidadeNome}}", especialidadeNome)            
            .Replace("{{RespostaId}}", respostaId.ToString())
            .Replace("{{Resposta}}", resposta);

        await SendEmailAsync(to, "Você tem uma nova Resposta a sua pergunta!", htmlBody);
    }
}