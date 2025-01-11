namespace RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

public record EspecialistaLocalAtendimentoResponse(
    int Id,
    string Nome,
    string Logradouro,
    string Complemento,
    string Bairro,
    string Cep,
    string Cidade,
    string Estado,
    decimal? Preco,
    string PrecoDescricao,
    string TipoAtendimento,
    string Telefone,
    string Whatsapp)
{
    public static EspecialistaLocalAtendimentoResponse MapFromEntity(Domain.Entities.EspecialistaLocalAtendimento local)
        => local is null
            ? default!
            : new EspecialistaLocalAtendimentoResponse(
                local.Id,
                local.Nome,
                local.Logradouro,
                local.Complemento,
                local.Bairro,
                local.Cep,
                local.Cidade,
                local.Estado,
                local.Preco,
                local.PrecoDescricao,
                local.TipoAtendimento,
                local.Telefone,
                local.Whatsapp);

    public static IReadOnlyList<EspecialistaLocalAtendimentoResponse>? MapFromEntity(IEnumerable<Domain.Entities.EspecialistaLocalAtendimento> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}

