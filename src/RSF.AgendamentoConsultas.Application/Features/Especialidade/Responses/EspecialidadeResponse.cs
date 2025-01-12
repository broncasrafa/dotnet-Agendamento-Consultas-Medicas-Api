namespace RSF.AgendamentoConsultas.Application.Features.Especialidade.Responses;

public class EspecialidadeResponse
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NomePlural { get; set; }
    public string Term { get; set; }
    public string Grupo { get; set; }

    public EspecialidadeResponse()
    {        
    }

    public static EspecialidadeResponse MapFromEntity(Domain.Entities.Especialidade especialidade)
        => especialidade is null
            ? default!
            : new EspecialidadeResponse 
            {
                Id = especialidade.EspecialidadeId,
                Nome = especialidade.Nome,
                NomePlural = especialidade.NomePlural,
                Term = especialidade.Term,
                Grupo = especialidade.EspecialidadeGrupo?.NomePlural!
            };

    public static IReadOnlyList<EspecialidadeResponse>? MapFromEntity(IEnumerable<Domain.Entities.Especialidade> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}