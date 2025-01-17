using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class PerguntaResposta
{
    public int PerguntaRespostaId { get; private set; }
    public int PerguntaId { get; private set; }
    public int EspecialistaId { get; private set; }
    public string Resposta { get; private set; }
    public DateTime CreatedAt { get; private set; }

    
    public Pergunta Pergunta { get; set; }
    public Especialista Especialista { get; set; }

    public ICollection<PerguntaRespostaReacao> Reacoes { get; set; }

    protected PerguntaResposta()
    {        
    }

    public PerguntaResposta(int perguntaId, int especialistaId, string resposta)
    {
        PerguntaId = perguntaId;
        EspecialistaId = especialistaId;
        Resposta = resposta;
        CreatedAt = DateTime.Now;

        DomainValidation.IdentifierGreaterThanZero(perguntaId, nameof(PerguntaId));
        DomainValidation.IdentifierGreaterThanZero(especialistaId, nameof(EspecialistaId));
        DomainValidation.NotNullOrEmpty(resposta, nameof(Resposta));
    }
}