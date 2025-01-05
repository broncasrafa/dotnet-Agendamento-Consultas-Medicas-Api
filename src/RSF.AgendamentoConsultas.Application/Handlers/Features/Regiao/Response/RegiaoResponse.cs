using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Response;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.Response;

public class RegiaoResponse
{
    public int RegiaoId { get; set; }
    public string Descricao { get; set; }
    public ICollection<EstadoResponse>? Estados { get; set; }

    public RegiaoResponse(int regiaoId, string descricao)
    {
        RegiaoId = regiaoId;
        Descricao = descricao;
        Estados = null;
    }
    public RegiaoResponse(int regiaoId, string descricao, ICollection<EstadoResponse> estados)
    {
        RegiaoId = regiaoId;
        Descricao = descricao;
        Estados = estados;
    }


    public static RegiaoResponse MapFromEntity(Domain.Entities.Regiao regiao)
    {
        if (regiao is not null)
        {
            if (regiao.Estados is not null)
            {
                var estados = regiao.Estados.Select(EstadoResponse.MapFromEntity).ToList();
                return new RegiaoResponse(regiao.RegiaoId, regiao.Descricao, estados);
            }

            return new RegiaoResponse(regiao.RegiaoId, regiao.Descricao);
        }

        return default!;
    }

    public static IReadOnlyList<RegiaoResponse> MapFromEntity(IReadOnlyList<Domain.Entities.Regiao> regioes)
        => regioes.Select(MapFromEntity).ToList();
}