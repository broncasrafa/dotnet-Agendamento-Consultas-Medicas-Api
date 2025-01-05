using RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Response;

public class EstadoResponse
{
    public int EstadoId { get; set; }
    public string Descricao { get; set; }
    public string Sigla { get; set; }
    public int RegiaoId { get; set; }

    public ICollection<CidadeResponse>? Cidades { get; set; }

    public EstadoResponse(int estadoId, string descricao, string sigla, int regiaoId)
    {
        EstadoId = estadoId;
        Descricao = descricao;
        Sigla = sigla;
        RegiaoId = regiaoId;
    }
    public EstadoResponse(int estadoId, string descricao, string sigla, int regiaoId, ICollection<CidadeResponse> cidades)
    {
        EstadoId = estadoId;
        Descricao = descricao;
        Sigla = sigla;
        RegiaoId = regiaoId;
        Cidades = cidades;
    }

    public static EstadoResponse MapFromEntity(Domain.Entities.Estado estado)
    {
        if (estado is not null)
        {
            if (estado.Cidades is not null)
            {
                var cidades = estado.Cidades.Select(CidadeResponse.MapFromEntity).ToList();
                return new EstadoResponse(estado.EstadoId, estado.Descricao, estado.Sigla, estado.RegiaoId, cidades);
            }

            return new EstadoResponse(estado.EstadoId, estado.Descricao, estado.Sigla, estado.RegiaoId);
        }
        return default!;
    }

    public static IReadOnlyList<EstadoResponse> MapFromEntity(IReadOnlyList<Domain.Entities.Estado> estados)
        => estados.Select(MapFromEntity).ToList();
}