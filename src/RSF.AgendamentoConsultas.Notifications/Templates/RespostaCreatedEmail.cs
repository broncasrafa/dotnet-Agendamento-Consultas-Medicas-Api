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
        var logo1 = await GetPreSignedUrlFromS3Async(bucketName: "rsfrancisco.agendamentoconsultas.images", key: "logo_1.png");
        var logo2 = await GetPreSignedUrlFromS3Async(bucketName: "rsfrancisco.agendamentoconsultas.images", key: "logo_3.png");

        htmlBody = htmlBody
            .Replace("{{PacienteNome}}", pacienteNome)
            .Replace("{{EspecialistaNome}}", especialistaNome)            
            .Replace("{{EspecialidadeNome}}", especialidadeNome)            
            .Replace("{{RespostaId}}", respostaId.ToString())
            .Replace("{{Resposta}}", resposta)
            .Replace("{{Logo1}}", logo1)
            .Replace("{{Logo2}}", logo2);

        await SendEmailAsync(to, "Você tem uma nova Resposta a sua pergunta!", htmlBody);
    }
}