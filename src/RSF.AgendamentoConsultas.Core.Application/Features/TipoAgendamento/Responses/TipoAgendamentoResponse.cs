namespace RSF.AgendamentoConsultas.Core.Application.Features.TipoAgendamento.Responses;

public record TipoAgendamentoResponse(int Id, string Descricao)
{
    public static TipoAgendamentoResponse MapFromEntity(Domain.Entities.TipoAgendamento tipo)
        => tipo is null ? default! : new TipoAgendamentoResponse(tipo.Id, tipo.Descricao);

    public static IReadOnlyList<TipoAgendamentoResponse> MapFromEntity(IReadOnlyList<Domain.Entities.TipoAgendamento> tipos)
        => tipos.Select(MapFromEntity).ToList();
}