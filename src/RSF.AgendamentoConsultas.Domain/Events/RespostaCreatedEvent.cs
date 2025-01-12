using RSF.AgendamentoConsultas.Domain.MessageBus.Events;

namespace RSF.AgendamentoConsultas.Domain.Events;

public class RespostaCreatedEvent : Event
{
    public int PacienteId { get; private set; }
    public string PacienteNome { get; private set; }
    public string PacienteEmail { get; private set; }
    public int EspecialistaId { get; private set; }
    public string EspecialistaNome { get; private set; }
    public string Resposta { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public RespostaCreatedEvent(int pacienteId, string pacienteNome, string pacienteEmail, int especialistaId, string especialistaNome, string resposta)
    {
        PacienteId = pacienteId;
        PacienteNome = pacienteNome;
        PacienteEmail = pacienteEmail;
        EspecialistaId = especialistaId;
        EspecialistaNome = especialistaNome;
        Resposta = resposta;
        CreatedAt = DateTime.UtcNow;
    }
}