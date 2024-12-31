using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class EspecialistaAvaliacao
{
    public int Id { get; private set; }
    public int EspecialistaId { get; private set; }
    public int PacienteId { get; private set; }
    public string Feedback { get; private set; }
    public int Score { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Especialista Especialista { get; set; }
    public Paciente Paciente { get; set; }

    public EspecialistaAvaliacao(int especialistaId, int pacienteId, string feedback, int score)
    {
        EspecialistaId = especialistaId;
        PacienteId = pacienteId;
        Feedback = feedback;
        Score = score;
        CreatedAt = DateTime.UtcNow;

        DomainValidation.IdentifierGreaterThanZero(especialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(pacienteId, nameof(PacienteId));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_SCORES, Score, nameof(Score));
    }
}