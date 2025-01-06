namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoConsulta.Responses;

public record TipoConsultaResponse(int Id, string Descricao)
{
    public static TipoConsultaResponse MapFromEntity(Domain.Entities.TipoConsulta tipo)
        => tipo is null ? default! : new TipoConsultaResponse(tipo.Id, tipo.Descricao);

    public static IReadOnlyList<TipoConsultaResponse> MapFromEntity(IReadOnlyList<Domain.Entities.TipoConsulta> tipos)
        => tipos.Select(MapFromEntity).ToList();
}