using RSF.AgendamentoConsultas.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Domain.Events;

public class PerguntaCreatedEvent : Event
{
    public int EspecialidadeId { get; private set; }
    public int PacienteId { get; private set; }
    public string PacienteEmail { get; private set; }
    public int PerguntaId { get; private set; }
    public string Pergunta { get; private set; }

    public PerguntaCreatedEvent(int especialidadeId, int pacienteId, string pacienteEmail, int perguntaId, string pergunta)
    {
        EspecialidadeId = especialidadeId;
        PacienteId = pacienteId;
        PacienteEmail = pacienteEmail;
        PerguntaId = perguntaId;
        Pergunta = pergunta;
    }
}