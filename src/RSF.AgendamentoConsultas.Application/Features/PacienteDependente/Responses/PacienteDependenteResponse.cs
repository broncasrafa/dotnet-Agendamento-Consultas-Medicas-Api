namespace RSF.AgendamentoConsultas.Application.Features.PacienteDependente.Responses;

public record PacienteDependenteResponse(
    int Id,
    string Nome,
    string Email,
    string Telefone,
    string Genero,
    string CPF,
    string DataNascimento,
    bool Ativo,
    IReadOnlyList<PacienteDependentePlanoMedicoResponse>? PlanosMedicos)
{
    public static PacienteDependenteResponse MapFromEntity(Domain.Entities.PacienteDependente entity)
        => entity is null ? default! : new PacienteDependenteResponse(
            entity.DependenteId,
            entity.Nome,
            entity.Email,
            entity.Telefone,
            entity.Genero,
            entity.CPF,
            entity.DataNascimento,
            entity.IsActive,
            PacienteDependentePlanoMedicoResponse.MapFromEntity(entity.PlanosMedicos));

    public static IReadOnlyList<PacienteDependenteResponse>? MapFromEntity(IEnumerable<Domain.Entities.PacienteDependente> collection)
        => collection is null || !collection.Any() ? default! : collection.Where(c => c.IsActive).Select(c => MapFromEntity(c)).ToList();
}