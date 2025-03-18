using RSF.AgendamentoConsultas.Core.Application.Features.Estado.Responses;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Cidade.Responses;

public class CidadeResponse(int cidadeId, string descricao, int estadoId, string? estado = null, string? siglaEstado = null)
{
    public int CidadeId { get; set; } = cidadeId;
    public string Descricao { get; set; } = descricao;
    public string DescricaoFormatada { get; set; } = default!;
    public int EstadoId { get; set; } = estadoId;
    public string? Estado { get; set; } = estado;
    public string? SiglaEstado { get; set; } = siglaEstado;

    public static CidadeResponse MapFromEntity(Domain.Entities.Cidade cidade)
    {
        if (cidade is null) return default!;

        if (cidade.Estado is not null)
        {
            return new CidadeResponse(cidade.CidadeId, cidade.Descricao, cidade.EstadoId, $"{cidade.Estado.Descricao}-{cidade.Estado.Sigla}", cidade.Estado.Sigla) { DescricaoFormatada = $"{cidade.Descricao}, {cidade.Estado.Sigla}" };
        }
        return new CidadeResponse(cidade.CidadeId, cidade.Descricao, cidade.EstadoId);
    }

    public static IReadOnlyList<CidadeResponse> MapFromEntity(IReadOnlyList<Domain.Entities.Cidade> cidades)
        => cidades is null ? default! : cidades.Select(MapFromEntity).ToList();
}