using RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.GetById;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.GetAll;

public class SelectEstadoResponse
{
    public int EstadoId { get; set; }
    public string Descricao { get; set; }
    public string Sigla { get; set; }
    public int RegiaoId { get; set; }

    public ICollection<SelectCidadeResponse>? Cidades { get; set; }

    public SelectEstadoResponse(int estadoId, string descricao, string sigla, int regiaoId)
    {
        EstadoId = estadoId;
        Descricao = descricao;
        Sigla = sigla;
        RegiaoId = regiaoId;
    }
    public SelectEstadoResponse(int estadoId, string descricao, string sigla, int regiaoId, ICollection<SelectCidadeResponse> cidades)
    {
        EstadoId = estadoId;
        Descricao = descricao;
        Sigla = sigla;
        RegiaoId = regiaoId;
        Cidades = cidades;
    }

    public static SelectEstadoResponse MapFromEntity(Domain.Entities.Estado estado)
    {
        if (estado is not null)
        {
            if (estado.Cidades is not null)
            {
                var cidades = estado.Cidades.Select(SelectCidadeResponse.MapFromEntity).ToList();
                return new SelectEstadoResponse(estado.EstadoId, estado.Descricao, estado.Sigla, estado.RegiaoId, cidades);
            }

            return new SelectEstadoResponse(estado.EstadoId, estado.Descricao, estado.Sigla, estado.RegiaoId);
        }
        return default!;
    }

    public static IReadOnlyList<SelectEstadoResponse> MapFromEntity(IReadOnlyList<Domain.Entities.Estado> estados)
        => estados.Select(MapFromEntity).ToList();
}