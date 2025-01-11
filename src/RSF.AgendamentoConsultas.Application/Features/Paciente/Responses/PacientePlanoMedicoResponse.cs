namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;

public record PacientePlanoMedicoResponse
(
    int Id,
    string NomePlano,
    string NumCartao,
    string ConvenioMedico
)
{
    public static PacientePlanoMedicoResponse MapFromEntity(Domain.Entities.PacientePlanoMedico entity)
        => entity is null ? default! : new PacientePlanoMedicoResponse(
            entity.PlanoMedicoId,
            entity.NomePlano,
            entity.NumCartao,
            entity.ConvenioMedico?.Nome);

    public static IReadOnlyList<PacientePlanoMedicoResponse>? MapFromEntity(IEnumerable<Domain.Entities.PacientePlanoMedico> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}