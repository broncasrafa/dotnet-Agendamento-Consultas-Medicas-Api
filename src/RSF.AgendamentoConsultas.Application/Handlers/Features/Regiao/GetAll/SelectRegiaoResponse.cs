using RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Regiao.GetAll;

public class SelectRegiaoResponse
{
    public int RegiaoId { get; set; }
    public string Descricao { get; set; }
    public ICollection<SelectEstadoResponse>? Estados { get; set; }

    public SelectRegiaoResponse(int regiaoId, string descricao)
    {
        RegiaoId = regiaoId;
        Descricao = descricao;
        Estados = null;
    }
    public SelectRegiaoResponse(int regiaoId, string descricao, ICollection<SelectEstadoResponse> estados)
    {
        RegiaoId = regiaoId;
        Descricao = descricao;
        Estados = estados;
    }


    public static SelectRegiaoResponse MapFromEntity(Domain.Entities.Regiao regiao)
    {
        if (regiao is not null)
        {
            if (regiao.Estados is not null)
            {
                var estados = regiao.Estados.Select(SelectEstadoResponse.MapFromEntity).ToList();
                return new SelectRegiaoResponse(regiao.RegiaoId, regiao.Descricao, estados);
            }

            return new SelectRegiaoResponse(regiao.RegiaoId, regiao.Descricao);
        }

        return default!;
    }

    public static IReadOnlyList<SelectRegiaoResponse> MapFromEntity(IReadOnlyList<Domain.Entities.Regiao> regioes)
        => regioes.Select(MapFromEntity).ToList();
}