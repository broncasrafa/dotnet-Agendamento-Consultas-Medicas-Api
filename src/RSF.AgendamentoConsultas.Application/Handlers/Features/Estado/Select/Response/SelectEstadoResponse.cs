namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Estado.Response;

public class SelectEstadoResponse
{
    public int EstadoId { get; set; }
    public string Descricao { get; set; }
    public string Sigla { get; set; }
    public int RegiaoId { get; set; }

    public SelectEstadoResponse(int estadoId, string descricao, string sigla, int regiaoId)
    {
        EstadoId = estadoId;
        Descricao = descricao;
        Sigla = sigla;
        RegiaoId = regiaoId;
    }
    
    public static SelectEstadoResponse MapFromEntity(Domain.Entities.Estado estado)
        => (estado is not null) ? new SelectEstadoResponse(estado.EstadoId, estado.Descricao, estado.Sigla, estado.RegiaoId) : default!;

    public static IReadOnlyList<SelectEstadoResponse> MapFromEntity(IReadOnlyList<Domain.Entities.Estado> estados)
        => estados.Select(MapFromEntity).ToList();
}