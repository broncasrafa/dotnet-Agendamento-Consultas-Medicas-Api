using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class PacienteEspecialistaFavoritos
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public int EspecialistaId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Especialista Especialista { get; set; }
    public Paciente Paciente { get; set; }

    public PacienteEspecialistaFavoritos(int id, int pacienteId, int especialistaId)
    {
        Id = id;
        PacienteId = pacienteId;
        EspecialistaId = especialistaId;
        CreatedAt = DateTime.Now;

        DomainValidation.IdentifierGreaterThanZero(id, nameof(Id));
        Validate();
    }
    public PacienteEspecialistaFavoritos(int pacienteId, int especialistaId)
    {
        PacienteId = pacienteId;
        EspecialistaId = especialistaId;
        CreatedAt = DateTime.Now;
        Validate();
    }

    private void Validate()
    {
        DomainValidation.IdentifierGreaterThanZero(EspecialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(PacienteId, nameof(PacienteId));
    }
}