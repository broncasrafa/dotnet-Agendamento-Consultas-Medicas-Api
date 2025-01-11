namespace RSF.AgendamentoConsultas.Application.Features.EspecialidadeGrupo.Responses;

public record EspecialidadeGrupoResponse(int Id, string Nome, string NomePlural)
{
    public static EspecialidadeGrupoResponse MapFromEntity(Domain.Entities.EspecialidadeGrupo grupo)
        => grupo is null ? default! : new EspecialidadeGrupoResponse(grupo.EspecialidadeGrupoId, grupo.Nome, grupo.NomePlural);

    public static IReadOnlyList<EspecialidadeGrupoResponse> MapFromEntity(IEnumerable<Domain.Entities.EspecialidadeGrupo> grupos)
        => grupos is null ? default! : grupos.Select(MapFromEntity).ToList();
}