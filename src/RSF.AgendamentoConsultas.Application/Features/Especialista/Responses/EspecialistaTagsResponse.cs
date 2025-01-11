namespace RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

public record EspecialistaTagsResponse(int Id, string Descricao)
{
    public static EspecialistaTagsResponse MapFromEntity(Domain.Entities.Tags tags)
        => tags is null ? default! : new EspecialistaTagsResponse(tags.TagId, tags.Descricao);

    public static IReadOnlyList<EspecialistaTagsResponse>? MapFromEntity(IEnumerable<Domain.Entities.Tags> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}