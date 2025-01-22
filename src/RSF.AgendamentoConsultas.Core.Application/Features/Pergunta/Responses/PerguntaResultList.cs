namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;

public class PerguntaResultList<T>
{
    public int PerguntaId { get; private set; }
    public IReadOnlyList<T> Results { get; private set; }

    public PerguntaResultList(int perguntaId, IReadOnlyList<T> results)
    {
        PerguntaId = perguntaId;
        Results = results ?? [];
    }
}