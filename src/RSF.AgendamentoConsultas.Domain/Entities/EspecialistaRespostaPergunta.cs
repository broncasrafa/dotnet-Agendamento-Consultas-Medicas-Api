using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class EspecialistaRespostaPergunta
{
    public int Id { get; private set; }
    public int PerguntaId { get; private set; }
    public string Resposta { get; private set; }
    public int? Likes { get; private set; }
    public int? Dislikes { get; private set; }
    public DateTime CreatedAt { get; private set; }

    
    public EspecialistaPergunta Pergunta { get; set; }

    protected EspecialistaRespostaPergunta()
    {        
    }

    public EspecialistaRespostaPergunta(int perguntaId, string resposta)
    {
        PerguntaId = perguntaId;
        Resposta = resposta;
        Likes = 0;
        Dislikes = 0;
        CreatedAt = DateTime.UtcNow;

        DomainValidation.IdentifierGreaterThanZero(perguntaId, nameof(PerguntaId));
        DomainValidation.NotNullOrEmpty(resposta, nameof(Resposta));
    }
}