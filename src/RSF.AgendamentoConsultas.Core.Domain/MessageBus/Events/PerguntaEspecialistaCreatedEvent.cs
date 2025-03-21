namespace RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

public class PerguntaEspecialistaCreatedEvent : Event
{
    public int EspecialistaId { get; private set; }
    public string EspecialistaNome { get; private set; }
    public string EspecialistaEmail { get; private set; }
    public string EspecialidadeNome { get; private set; }
    public int PacienteId { get; private set; }
    public string PacienteNome { get; private set; }
    public string PacienteEmail { get; private set; }
    public int PerguntaId { get; private set; }
    public string Pergunta { get; private set; }

    public PerguntaEspecialistaCreatedEvent(
        int especialistaId, 
        string especialistaNome, 
        string especialistaEmail,
        string especialidadeNome,
        int pacienteId, 
        string pacienteNome, 
        string pacienteEmail, 
        int perguntaId, 
        string pergunta)
    {
        EspecialistaId = especialistaId;
        EspecialistaNome = especialistaNome;
        EspecialistaEmail = especialistaEmail;
        EspecialidadeNome = especialidadeNome;
        PacienteId = pacienteId;
        PacienteNome = pacienteNome;
        PacienteEmail = pacienteEmail;
        PerguntaId = perguntaId;
        Pergunta = pergunta;
    }
}