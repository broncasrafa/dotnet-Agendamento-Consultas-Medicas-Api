namespace RSF.AgendamentoConsultas.Application.Handlers.Features.TipoStatusConsulta.Responses;

public record TipoStatusConsultaResponse(int Id, string Descricao)
{
    public static TipoStatusConsultaResponse MapFromEntity(Domain.Entities.TipoStatusConsulta tipo)
        => tipo is null ? default! : new TipoStatusConsultaResponse(tipo.Id, tipo.Descricao);

    public static IReadOnlyList<TipoStatusConsultaResponse> MapFromEntity(IReadOnlyList<Domain.Entities.TipoStatusConsulta> tipos)
        => tipos.Select(MapFromEntity).ToList();
}