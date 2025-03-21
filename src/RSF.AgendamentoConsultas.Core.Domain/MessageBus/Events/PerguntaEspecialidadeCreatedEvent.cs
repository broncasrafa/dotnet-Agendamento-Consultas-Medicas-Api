namespace RSF.AgendamentoConsultas.Core.Domain.MessageBus.Events;

public class PerguntaEspecialidadeCreatedEvent : Event
{
    public int EspecialidadeId { get; private set; }
    public string EspecialidadeNome { get; private set; }
    public int PacienteId { get; private set; }
    public string PacienteNome { get; private set; }
    public string PacienteEmail { get; private set; }
    public int PerguntaId { get; private set; }
    public string Pergunta { get; private set; }

    public PerguntaEspecialidadeCreatedEvent(int especialidadeId, string especialidadeNome, int pacienteId, string pacienteNome, string pacienteEmail, int perguntaId, string pergunta)
    {
        EspecialidadeId = especialidadeId;
        EspecialidadeNome = especialidadeNome;
        PacienteId = pacienteId;
        PacienteNome = pacienteNome;
        PacienteEmail = pacienteEmail;
        PerguntaId = perguntaId;
        Pergunta = pergunta;
    }
}