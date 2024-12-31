using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class EspecialistaRespostaPergunta
{
    public int Id { get; private set; }
    public int PerguntaId { get; private set; }
    public int EspecialistaId { get; private set; }
    public string Resposta { get; private set; }
    public int? Likes { get; private set; }
    public int? Dislikes { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Especialista Especialista { get; set; }
    public EspecialistaPergunta Pergunta { get; set; }

    public EspecialistaRespostaPergunta(int perguntaId, int especialistaId, string resposta)
    {
        PerguntaId = perguntaId;
        EspecialistaId = especialistaId;
        Resposta = resposta;
        CreatedAt = DateTime.UtcNow;

        DomainValidation.IdentifierGreaterThanZero(perguntaId, nameof(PerguntaId));
        DomainValidation.IdentifierGreaterThanZero(especialistaId, nameof(EspecialistaId));
        DomainValidation.NotNullOrEmpty(resposta, nameof(Resposta));
    }
}