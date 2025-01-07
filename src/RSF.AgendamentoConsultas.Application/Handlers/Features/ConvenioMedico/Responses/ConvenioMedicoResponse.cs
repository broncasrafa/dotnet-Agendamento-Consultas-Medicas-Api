using RSF.AgendamentoConsultas.Application.Handlers.Features.Cidade.Responses;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.ConvenioMedico.Responses;

public class ConvenioMedicoResponse
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public IReadOnlyList<CidadeResponse>? CidadesAtendidas { get; private set; }

    public ConvenioMedicoResponse(int id, string nome, IReadOnlyList<CidadeResponse>? cidadesAtendidas = default!)
    {
        Id = id;
        Nome = nome;
        CidadesAtendidas = cidadesAtendidas;
    }

    public static ConvenioMedicoResponse MapFromEntity(Domain.Entities.ConvenioMedico convenio)
    {
        var cidadesAtendidas = CidadeResponse.MapFromEntity(convenio.CidadesAtendidas?.ToList());
        return new ConvenioMedicoResponse(convenio.ConvenioMedicoId, convenio.Nome, cidadesAtendidas);
    }

    public static IReadOnlyList<ConvenioMedicoResponse>? MapFromEntity(IEnumerable<Domain.Entities.ConvenioMedico> convenios)
        => (convenios is null || !convenios.Any()) ? null : convenios.Select(MapFromEntity).ToList();
}