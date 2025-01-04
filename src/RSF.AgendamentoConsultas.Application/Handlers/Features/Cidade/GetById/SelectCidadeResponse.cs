using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.GetById;

public class SelectCidadeResponse(int cidadeId, string descricao, int estadoId, string? estado = null)
{
    public int CidadeId { get; set; } = cidadeId;
    public string Descricao { get; set; } = descricao;
    public int EstadoId { get; set; } = estadoId;
    public string? Estado { get; set; } = estado;

    public static SelectCidadeResponse MapFromEntity(Domain.Entities.Cidade cidade)
    {
        if (cidade is null) return default!;

        if (cidade.Estado is not null)
        {
            var estado = SelectEstadoResponse.MapFromEntity(cidade.Estado);
            return new SelectCidadeResponse(cidade.CidadeId, cidade.Descricao, cidade.EstadoId, $"{estado.Descricao}-{estado.Sigla}");
        }
        return new SelectCidadeResponse(cidade.CidadeId, cidade.Descricao, cidade.EstadoId);
    }
}