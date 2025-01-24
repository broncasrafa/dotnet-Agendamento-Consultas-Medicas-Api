using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

public class AgendamentoCanceledByEspecialistaEmail : MailTemplateBase
{
    public AgendamentoCanceledByEspecialistaEmail(IMailSender mailSender, IAmazonS3 s3Client) 
        : base(mailSender, s3Client)
    {
    }

    public async Task SendEmailAsync(
        MailTo to,
        string pacienteNome,
        string especialistaNome,
        string especialidade,
        string dataConsulta,
        string horarioConsulta,
        string localAtendimento)
    {
        var htmlBody = await GetHtmlTemplateFromS3Async(key: "agendamento_canceled_by_especialista_template.html");
        htmlBody = htmlBody
            .Replace("{{PacienteNome}}", pacienteNome)
            .Replace("{{EspecialistaNome}}", especialistaNome)
            .Replace("{{Especialidade}}", especialidade)
            .Replace("{{DataConsulta}}", dataConsulta)
            .Replace("{{HorarioConsulta}}", horarioConsulta)
            .Replace("{{LocalAtendimento}}", localAtendimento);

        await SendEmailAsync(to, "Cancelamento de Agendamento!", htmlBody);
    }
}