namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Paciente.Responses;

public record PacienteDependenteResponse(
    int Id,
    string Nome,
    string Telefone,
    string DataNascimento
    )
{
    public static PacienteDependenteResponse MapFromEntity(Domain.Entities.PacienteDependente dependente)
        => dependente is null ? default! : new PacienteDependenteResponse(
            dependente.DependenteId, 
            dependente.Nome, 
            dependente.Telefone, 
            dependente.DataNascimento);

    public static IReadOnlyList<PacienteDependenteResponse>? MapFromEntity(IEnumerable<Domain.Entities.PacienteDependente> collection)
        => (collection is null || !collection.Any()) ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}