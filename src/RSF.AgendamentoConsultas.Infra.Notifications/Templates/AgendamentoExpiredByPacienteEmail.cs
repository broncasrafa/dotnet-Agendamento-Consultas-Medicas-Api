using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

public class AgendamentoExpiredByPacienteEmail : MailTemplateBase
{
    public AgendamentoExpiredByPacienteEmail(IMailSender mailSender, IAmazonS3 s3Client) 
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
        string localAtendimento,
        string notaCancelamento)
    {
        var htmlBody = await GetHtmlTemplateFromS3Async(key: "agendamento_expired_by_paciente_template.html");
        htmlBody = htmlBody
            .Replace("{{PacienteNome}}", pacienteNome)
            .Replace("{{EspecialistaNome}}", especialistaNome)
            .Replace("{{Especialidade}}", especialidade)
            .Replace("{{DataConsulta}}", dataConsulta)
            .Replace("{{HorarioConsulta}}", horarioConsulta)
            .Replace("{{LocalAtendimento}}", localAtendimento)
            .Replace("{{NotaCancelamento}}", notaCancelamento);

        await SendEmailAsync(to, "Agendamento Expirado!", htmlBody);
    }
}