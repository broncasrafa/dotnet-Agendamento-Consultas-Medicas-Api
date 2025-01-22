using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using Amazon.S3;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

public class AgendamentoCreatedEmail : MailTemplateBase
{
    public AgendamentoCreatedEmail(IMailSender mailSender, IAmazonS3 s3Client) 
        : base(mailSender, s3Client)
    {
    }

    public async Task SendEmailAsync(
        MailTo to,
        int agendamentoConsultaId,
        string dataAgendamento,
        string tipoAgendamento,
        string tipoConsulta,
        string especialidade,
        string especialista,
        string especialistaEmail,
        string pacienteNome,
        string convenioMedico,
        string motivoConsulta,
        string dataConsulta,
        string horarioConsulta,
        bool primeiraVez,
        string localAtendimentoNome,
        string localAtendimentoLogradouro,
        string localAtendimentoComplemento,
        string localAtendimentoBairro,
        string localAtendimentoCep,
        string localAtendimentoCidade,
        string localAtendimentoEstado)
    {

        var htmlBody = await GetHtmlTemplateFromS3Async(bucketName: "rsfrancisco.agendamentoconsultas.emailtemplates", key: "agendamento_created_template.html");

        htmlBody = htmlBody
            .Replace("{{AgendamentoConsultaId}}", agendamentoConsultaId.ToString())
            .Replace("{{DataAgendamento}}", dataAgendamento)
            .Replace("{{TipoAgendamento}}", tipoAgendamento)
            .Replace("{{TipoConsulta}}", tipoConsulta)
            .Replace("{{EspecialidadeNome}}", especialidade)
            .Replace("{{EspecialistaNome}}", especialista)
            .Replace("{{EspecialistaEmail}}", especialistaEmail)
            .Replace("{{PacienteNome}}", pacienteNome)
            .Replace("{{ConvenioMedico}}", convenioMedico)
            .Replace("{{MotivoConsulta}}", motivoConsulta)
            .Replace("{{DataConsulta}}", dataConsulta)
            .Replace("{{HorarioConsulta}}", horarioConsulta)
            .Replace("{{PrimeiraVez}}", primeiraVez ? "SIM":"NÃO")
            .Replace("{{LocalAtendimentoNome}}", localAtendimentoNome)
            .Replace("{{LocalAtendimentoLogradouro}}", localAtendimentoLogradouro)
            .Replace("{{LocalAtendimentoComplemento}}", localAtendimentoComplemento)
            .Replace("{{LocalAtendimentoBairro}}", localAtendimentoBairro)
            .Replace("{{LocalAtendimentoCep}}", localAtendimentoCep)
            .Replace("{{LocalAtendimentoCidade}}", localAtendimentoCidade)
            .Replace("{{LocalAtendimentoEstado}}", localAtendimentoEstado);

        await SendEmailAsync(to, "Você tem uma nova Solicitação de Agendamento!", htmlBody);
    }
}