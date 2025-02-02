using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class EspecialistaAvaliacao
{
    public int Id { get; private set; }
    public int EspecialistaId { get; private set; }
    public int? TagId { get; private set; }
    public int PacienteId { get; private set; }
    public string Feedback { get; private set; }
    public int Score { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Especialista Especialista { get; set; }
    public Paciente Paciente { get; set; }
    public Tags Marcacao { get; set; }

    protected EspecialistaAvaliacao()
    {        
    }

    public EspecialistaAvaliacao(int especialistaId, int pacienteId, string feedback, int score, int? tagId)
    {
        EspecialistaId = especialistaId;
        PacienteId = pacienteId;
        Feedback = feedback;
        Score = score;
        TagId = tagId;
        CreatedAt = DateTime.Now;

        DomainValidation.NotNullOrEmpty(feedback, nameof(Feedback));
        DomainValidation.IdentifierGreaterThanZero(especialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(pacienteId, nameof(PacienteId));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_SCORES, Score, nameof(Score));
        if (tagId.HasValue)
            DomainValidation.IdentifierGreaterThanZero(TagId, nameof(TagId));
    }
}