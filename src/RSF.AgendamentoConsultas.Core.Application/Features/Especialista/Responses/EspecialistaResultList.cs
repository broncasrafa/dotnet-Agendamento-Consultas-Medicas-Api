namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

public class EspecialistaResultList<T>
{
    public int EspecialistaId { get; private set; }
    public IReadOnlyList<T> Results { get; private set; }

    public EspecialistaResultList(int especialistaId, IReadOnlyList<T> results)
    {
        EspecialistaId = especialistaId;
        Results = results ?? [];
    }
}