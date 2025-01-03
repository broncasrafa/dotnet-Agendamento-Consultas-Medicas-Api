namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.Response;

public class SelectCidadeResponse(int cidadeId, string descricao, int estadoId)
{
    public int CidadeId { get; set; } = cidadeId;
    public string Descricao { get; set; } = descricao;
    public int EstadoId { get; set; } = estadoId;

    public static SelectCidadeResponse MapFromEntity(Domain.Entities.Cidade cidade)
        => (cidade is not null) ? new SelectCidadeResponse(cidade.CidadeId, cidade.Descricao, cidade.EstadoId) : default!;

    public static IReadOnlyList<SelectCidadeResponse> MapFromEntity(IReadOnlyList<Domain.Entities.Cidade> cidades)
        => cidades.Select(MapFromEntity).ToList();
}