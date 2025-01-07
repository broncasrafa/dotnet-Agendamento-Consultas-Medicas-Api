namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;

public record EspecialistaRespostaPerguntaResponse(
    int Id,
    int PerguntaId,
    string Resposta,
    int? Likes,
    int? Dislikes,
    DateTime CreatedAt)
{
    public static EspecialistaRespostaPerguntaResponse MapFromEntity(Domain.Entities.EspecialistaRespostaPergunta resposta)
        => resposta is null
            ? default!
            : new EspecialistaRespostaPerguntaResponse(
                resposta.Id,
                resposta.PerguntaId,
                resposta.Resposta,
                resposta.Likes,
                resposta.Dislikes,
                resposta.CreatedAt);

    public static IReadOnlyList<EspecialistaRespostaPerguntaResponse> MapFromEntity(IEnumerable<Domain.Entities.EspecialistaRespostaPergunta> collection)
        => (collection is null || !collection.Any()) ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}