namespace RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

public record EspecialistaPerguntaResponse(
    int Id,
    string Pergunta,
    string Especialidade,
    string Paciente,
    DateTime CreatedAt)
{
    public static EspecialistaPerguntaResponse MapFromEntity(Domain.Entities.Pergunta entity)
        => entity is null
            ? default!
            : new EspecialistaPerguntaResponse(
                entity.PerguntaId,
                entity.Texto,
                entity.Especialidade?.Nome,
                entity.Paciente.Nome,
                entity.CreatedAt);

    public static IReadOnlyList<EspecialistaPerguntaResponse>? MapFromEntity(IEnumerable<Domain.Entities.Pergunta> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}