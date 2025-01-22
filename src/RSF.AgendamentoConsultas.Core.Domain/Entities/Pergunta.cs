using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class Pergunta
{
    public int PerguntaId { get; private set; }
    public int EspecialidadeId { get; private set; }
    public int PacienteId { get; private set; }
    public string Texto { get; private set; }
    public bool? TermosUsoPolitica { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Especialidade Especialidade { get; set; }
    public Paciente Paciente { get; set; }
    public ICollection<PerguntaResposta> Respostas { get; set; }


    protected Pergunta()
    {        
    }

    public Pergunta(int especialidadeId, int pacienteId, string pergunta, bool termosUsoPolitica)
    {
        EspecialidadeId = especialidadeId;
        PacienteId = pacienteId;
        Texto = pergunta;
        TermosUsoPolitica = termosUsoPolitica;
        CreatedAt = DateTime.Now;

        DomainValidation.IdentifierGreaterThanZero(especialidadeId, nameof(EspecialidadeId));
        DomainValidation.IdentifierGreaterThanZero(pacienteId, nameof(PacienteId));
        DomainValidation.NotNullOrEmpty(pergunta, nameof(Texto));
    }
}