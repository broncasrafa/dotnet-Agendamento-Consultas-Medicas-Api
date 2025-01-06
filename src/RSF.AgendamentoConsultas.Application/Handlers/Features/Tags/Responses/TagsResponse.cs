﻿namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Tags.Responses;

public class TagsResponse
{
    public int Id { get; set; }
    public string Descricao { get; set; }

    public TagsResponse(int id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }


    public static TagsResponse MapFromEntity(Domain.Entities.Tags tags)
        => tags is null ? default! : new TagsResponse(tags.TagId, tags.Descricao);

    public static IReadOnlyList<TagsResponse> MapFromEntity(IReadOnlyList<Domain.Entities.Tags> tags)
        => tags.Select(MapFromEntity).ToList();
}