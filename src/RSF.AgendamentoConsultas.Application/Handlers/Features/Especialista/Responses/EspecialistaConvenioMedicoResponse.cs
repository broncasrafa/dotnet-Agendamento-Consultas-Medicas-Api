namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;

public record EspecialistaConvenioMedicoResponse(int Id, string Nome)
{
    public static EspecialistaConvenioMedicoResponse MapFromEntity(Domain.Entities.ConvenioMedico convenio)
        => (convenio is null) ? default! : new EspecialistaConvenioMedicoResponse(convenio.ConvenioMedicoId, convenio.Nome);

    public static IReadOnlyList<EspecialistaConvenioMedicoResponse>? MapFromEntity(IEnumerable<Domain.Entities.ConvenioMedico> collection)
        => (collection is null || !collection.Any()) ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}