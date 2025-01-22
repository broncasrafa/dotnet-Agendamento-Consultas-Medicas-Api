namespace RSF.AgendamentoConsultas.Core.Application.Features.PacienteDependente.Responses;

public class PacienteDependenteResultList<T>
{
    public int DependenteId { get; private set; }
    public int PacientePrincipalId { get; private set; }
    public IReadOnlyList<T> Results { get; private set; }

    public PacienteDependenteResultList(int dependenteId, int pacientePrincipalId, IReadOnlyList<T> results)
    {
        DependenteId = dependenteId;
        PacientePrincipalId = pacientePrincipalId;
        Results = results ?? [];
    }
}