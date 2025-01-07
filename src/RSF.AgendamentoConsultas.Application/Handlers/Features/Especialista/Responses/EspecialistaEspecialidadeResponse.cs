namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;

public record EspecialistaEspecialidadeResponse(
    int Id,
    string Nome,
    string NomePlural,
    string Term,
    string Grupo)
{
    public static EspecialistaEspecialidadeResponse MapFromEntity(Domain.Entities.Especialidade especialidade)
        => especialidade is null
            ? default!
            : new EspecialistaEspecialidadeResponse(
                especialidade.EspecialidadeId,
                especialidade.Nome,
                especialidade.NomePlural,
                especialidade.Term,
                especialidade.EspecialidadeGrupo?.NomePlural);

    public static IReadOnlyList<EspecialistaEspecialidadeResponse>? MapFromEntity(IEnumerable<Domain.Entities.Especialidade> collection)
        => (collection is null || !collection.Any()) ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}