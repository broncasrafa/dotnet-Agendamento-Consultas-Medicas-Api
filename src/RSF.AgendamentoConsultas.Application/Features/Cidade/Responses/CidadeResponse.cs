using RSF.AgendamentoConsultas.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Cidade.Responses;

public class CidadeResponse(int cidadeId, string descricao, int estadoId, string? estado = null)
{
    public int CidadeId { get; set; } = cidadeId;
    public string Descricao { get; set; } = descricao;
    public int EstadoId { get; set; } = estadoId;
    public string? Estado { get; set; } = estado;

    public static CidadeResponse MapFromEntity(Domain.Entities.Cidade cidade)
    {
        if (cidade is null) return default!;

        if (cidade.Estado is not null)
        {
            var estado = EstadoResponse.MapFromEntity(cidade.Estado);
            return new CidadeResponse(cidade.CidadeId, cidade.Descricao, cidade.EstadoId, $"{estado.Descricao}-{estado.Sigla}");
        }
        return new CidadeResponse(cidade.CidadeId, cidade.Descricao, cidade.EstadoId);
    }

    public static IReadOnlyList<CidadeResponse> MapFromEntity(IReadOnlyList<Domain.Entities.Cidade> cidades)
        => cidades is null ? default! : cidades.Select(MapFromEntity).ToList();
}