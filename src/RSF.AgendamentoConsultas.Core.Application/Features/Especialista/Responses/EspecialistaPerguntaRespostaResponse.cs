namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

public record EspecialistaPerguntaRespostaResponse(
    int Id,
    string Resposta,    
    DateTime CreatedAt,
    EspecialistaPerguntaResponse Pergunta)
{
    public static EspecialistaPerguntaRespostaResponse MapFromEntity(Domain.Entities.PerguntaResposta entity)
        => entity is null
            ? default!
            : new EspecialistaPerguntaRespostaResponse(
                entity.PerguntaRespostaId,
                entity.Resposta,
                entity.CreatedAt,
                EspecialistaPerguntaResponse.MapFromEntity(entity.Pergunta));

    public static IReadOnlyList<EspecialistaPerguntaRespostaResponse> MapFromEntity(IEnumerable<Domain.Entities.PerguntaResposta> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}