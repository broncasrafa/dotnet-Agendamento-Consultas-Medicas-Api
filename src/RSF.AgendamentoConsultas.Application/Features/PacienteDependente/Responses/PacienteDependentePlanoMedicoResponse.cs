namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;

public record PacienteDependentePlanoMedicoResponse
(
    int Id,
    string NomePlano,
    string NumCartao,
    string ConvenioMedico
)
{
    public static PacienteDependentePlanoMedicoResponse MapFromEntity(Domain.Entities.PacienteDependentePlanoMedico entity)
        => entity is null ? default! : new PacienteDependentePlanoMedicoResponse(
            entity.PlanoMedicoId,
            entity.NomePlano,
            entity.NumCartao,
            entity.ConvenioMedico?.Nome);

    public static IReadOnlyList<PacienteDependentePlanoMedicoResponse>? MapFromEntity(IEnumerable<Domain.Entities.PacienteDependentePlanoMedico> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}