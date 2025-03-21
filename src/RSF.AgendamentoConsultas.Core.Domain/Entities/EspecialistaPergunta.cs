using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class EspecialistaPergunta
{    
    public int Id { get; set; }
    public int EspecialistaId { get; set; }
    public int PerguntaId { get; set; }

    public Especialista Especialista { get; set; }
    public Pergunta Pergunta { get; set; }

    protected EspecialistaPergunta()
    {        
    }

    public EspecialistaPergunta(int id, int especialistaId, int perguntaId)
    {
        Id = id;
        EspecialistaId = especialistaId;
        PerguntaId = perguntaId;

        DomainValidation.IdentifierGreaterThanZero(id, nameof(Id));
        Validate();
    }
    public EspecialistaPergunta(int especialistaId, int perguntaId)
    {
        EspecialistaId = especialistaId;
        PerguntaId = perguntaId;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.IdentifierGreaterThanZero(EspecialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(PerguntaId, nameof(PerguntaId));
    }
}