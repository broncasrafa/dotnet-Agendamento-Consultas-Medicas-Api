using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

public class PerguntaEspecialistaCreatedEmail : MailTemplateBase
{
    public PerguntaEspecialistaCreatedEmail(IMailSender mailSender, IAmazonS3 s3Client) 
        : base(mailSender, s3Client)
    {
    }

    public async Task SendEmailAsync(
        MailTo to,
        string pacienteNome,
        string especialistaNome,
        string especialidadeNome,
        string pergunta,
        int perguntaId)
    {
        var htmlBody = await GetHtmlTemplateFromS3Async(key: "pergunta_especialista_created_template.html");
        
        htmlBody = htmlBody
            .Replace("{{PacienteNome}}", pacienteNome)
            .Replace("{{EspecialistaNome}}", especialistaNome)
            .Replace("{{EspecialidadeNome}}", especialidadeNome)
            .Replace("{{Pergunta}}", pergunta)
            .Replace("{{PerguntaId}}", perguntaId.ToString());
        
        await SendEmailAsync(to, "Você tem uma nova Pergunta!", htmlBody);
    }
}