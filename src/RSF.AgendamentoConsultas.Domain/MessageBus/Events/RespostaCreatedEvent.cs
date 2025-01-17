namespace RSF.AgendamentoConsultas.Domain.MessageBus.Events;

public class RespostaCreatedEvent : Event
{
    public int PacienteId { get; private set; }
    public string PacienteNome { get; private set; }
    public string PacienteEmail { get; private set; }
    public string EspecialidadeNome { get; private set; }
    public int EspecialistaId { get; private set; }
    public string EspecialistaNome { get; private set; }
    public int RespostaId { get; private set; }
    public string Resposta { get; private set; }

    public RespostaCreatedEvent(int pacienteId, string pacienteNome, string pacienteEmail, string especialidadeNome, int especialistaId, string especialistaNome, int respostaId, string resposta)
    {
        PacienteId = pacienteId;
        PacienteNome = pacienteNome;
        PacienteEmail = pacienteEmail;
        EspecialidadeNome = especialidadeNome;
        EspecialistaId = especialistaId;
        EspecialistaNome = especialistaNome;
        RespostaId = respostaId;
        Resposta = resposta;
    }
}