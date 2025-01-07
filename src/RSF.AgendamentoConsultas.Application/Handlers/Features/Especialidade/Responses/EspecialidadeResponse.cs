namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialidade.Responses;

public record EspecialidadeResponse(
    int Id,
    string Nome,
    string NomePlural,
    string Term,
    string Grupo)
{
    public static EspecialidadeResponse MapFromEntity(Domain.Entities.Especialidade especialidade)
        => especialidade is null
            ? default!
            : new EspecialidadeResponse(
                especialidade.EspecialidadeId,
                especialidade.Nome,
                especialidade.NomePlural,
                especialidade.Term,
                especialidade.EspecialidadeGrupo?.NomePlural);

    public static IReadOnlyList<EspecialidadeResponse>? MapFromEntity(IEnumerable<Domain.Entities.Especialidade> especialidades)
    {
        if (especialidades is null || !especialidades.Any()) return default!;

        return especialidades.Select(MapFromEntity).ToList();
    }
}