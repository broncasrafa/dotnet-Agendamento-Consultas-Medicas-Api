using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Application.Handlers.Features.Especialista.Responses;

public record EspecialistaPerguntaResponse(
    int Id,
    string Paciente,
    string Titulo,
    string Pergunta,
    DateTime CreatedAt,
    IReadOnlyList<EspecialistaRespostaPerguntaResponse> Respostas)
{
    public static EspecialistaPerguntaResponse MapFromEntity(EspecialistaPergunta pergunta)
        => pergunta is null
            ? default!
            : new EspecialistaPerguntaResponse(
                pergunta.Id,
                pergunta.Paciente.Nome,
                pergunta.Titulo,
                pergunta.Pergunta,
                pergunta.CreatedAt,
                EspecialistaRespostaPerguntaResponse.MapFromEntity(pergunta.Respostas));

    public static IReadOnlyList<EspecialistaPerguntaResponse>? MapFromEntity(IEnumerable<EspecialistaPergunta> collection)
        => (collection is null || !collection.Any()) ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}