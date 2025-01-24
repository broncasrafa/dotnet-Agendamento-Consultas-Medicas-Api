using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

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
        var htmlBody = await GetHtmlTemplateFromS3Async(key: "pergunta_created_template.html");
        
        htmlBody = htmlBody
            .Replace("{{PacienteNome}}", pacienteNome)
            .Replace("{{EspecialidadeNome}}", especialidadeNome)
            .Replace("{{Pergunta}}", pergunta)
            .Replace("{{PerguntaId}}", perguntaId.ToString())
            .Replace("{{EspecialistaId}}", especialistaId.ToString())
            .Replace("{{EspecialistaNome}}", especialistaNome);
        
        await SendEmailAsync(to, "Você tem uma nova Pergunta!", htmlBody);
    }
}