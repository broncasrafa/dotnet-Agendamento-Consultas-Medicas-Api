namespace RSF.AgendamentoConsultas.Application.Handlers.Features.PacienteDependente.Responses;

public record PacienteDependenteResponse(
    int Id,
    string Nome,
    string Email,
    string Telefone,
    string Genero,
    string CPF,
    string DataNascimento,
    string PacientePrincipal,
    IReadOnlyList<PacienteDependentePlanoMedicoResponse>? PlanosMedicos)
{
    public static PacienteDependenteResponse MapFromEntity(Domain.Entities.PacienteDependente dependente)
        => dependente is null ? default! : new PacienteDependenteResponse(
            dependente.DependenteId,
            dependente.Nome,
            dependente.Email,
            dependente.Telefone,
            dependente.Genero,
            dependente.CPF,
            dependente.DataNascimento,
            dependente.Paciente?.Nome,
            PacienteDependentePlanoMedicoResponse.MapFromEntity(dependente.PlanosMedicos));

    public static IReadOnlyList<PacienteDependenteResponse>? MapFromEntity(IEnumerable<Domain.Entities.PacienteDependente> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}