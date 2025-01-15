using RSF.AgendamentoConsultas.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Notifications.Templates;

public class PerguntaCreatedEmail : MailTemplateBase
{
    public PerguntaCreatedEmail(IMailSender mailSender, IAmazonS3 s3Client) 
        : base(mailSender, s3Client)
    {
    }

    public async Task SendEmailAsync(
        MailTo to,
        string pacienteNome,
        string especialidadeNome,
        string pergunta,
        int perguntaId,
        int especialistaId,
        string especialistaNome)
    {
        var htmlBody = await GetHtmlTemplateFromS3Async(bucketName: "rsfrancisco.agendamentoconsultas.emailtemplates", key: "pergunta_created_template.html");
        var logo1 = await GetPreSignedUrlFromS3Async(bucketName: "rsfrancisco.agendamentoconsultas.images", key: "logo_1.png");
        var logo2 = await GetPreSignedUrlFromS3Async(bucketName: "rsfrancisco.agendamentoconsultas.images", key: "logo_3.png");

        // Realiza o replace com os valores específicos
        htmlBody = htmlBody
            .Replace("{{PacienteNome}}", pacienteNome)
            .Replace("{{EspecialidadeNome}}", especialidadeNome)
            .Replace("{{Pergunta}}", pergunta)
            .Replace("{{PerguntaId}}", perguntaId.ToString())
            .Replace("{{EspecialistaId}}", especialistaId.ToString())
            .Replace("{{EspecialistaNome}}", especialistaNome)
            .Replace("{{Logo1}}", logo1)
            .Replace("{{Logo2}}", logo2);
        
        await SendEmailAsync(to, "Você tem uma nova Pergunta!", htmlBody);
    }
}