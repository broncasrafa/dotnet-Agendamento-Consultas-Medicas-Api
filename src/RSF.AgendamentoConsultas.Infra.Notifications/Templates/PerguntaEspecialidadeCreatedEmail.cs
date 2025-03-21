using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

public class PerguntaEspecialidadeCreatedEmail : MailTemplateBase
{
    public PerguntaEspecialidadeCreatedEmail(IMailSender mailSender, IAmazonS3 s3Client) 
        : base(mailSender, s3Client)
    {
    }

    public async Task SendEmailAsync(
        List<MailTo> toList,
        string pacienteNome,
        string especialidadeNome,
        string pergunta,
        int perguntaId)
    {
        var htmlBody = await GetHtmlTemplateFromS3Async(key: "pergunta_especialidade_created_template.html");
        
        htmlBody = htmlBody
            .Replace("{{PacienteNome}}", pacienteNome)
            .Replace("{{EspecialidadeNome}}", especialidadeNome)
            .Replace("{{Pergunta}}", pergunta)
            .Replace("{{PerguntaId}}", perguntaId.ToString());
        
        await SendEmailAsync(toList, "Você tem uma nova Pergunta!", htmlBody);
    }
}