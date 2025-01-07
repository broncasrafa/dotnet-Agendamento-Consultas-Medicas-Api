using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class EspecialistaPergunta
{
    public int Id { get; private set; }
    public int EspecialistaId { get; private set; }
    public int PacienteId { get; private set; }
    public string Titulo { get; private set; }
    public string Pergunta { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Especialista Especialista { get; set; }
    public Paciente Paciente { get; set; }
    public ICollection<EspecialistaRespostaPergunta> Respostas { get; set; }


    protected EspecialistaPergunta()
    {        
    }

    public EspecialistaPergunta(int especialistaId, int pacienteId, string titulo, string pergunta)
    {
        EspecialistaId = especialistaId;
        PacienteId = pacienteId;
        Titulo = titulo;
        Pergunta = pergunta;
        CreatedAt = DateTime.UtcNow;

        DomainValidation.IdentifierGreaterThanZero(especialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(pacienteId, nameof(PacienteId));
        DomainValidation.NotNullOrEmpty(titulo, nameof(Titulo));
        DomainValidation.NotNullOrEmpty(pergunta, nameof(Pergunta));
    }
}