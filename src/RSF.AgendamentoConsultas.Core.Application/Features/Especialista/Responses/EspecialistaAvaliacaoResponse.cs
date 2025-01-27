using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

public record EspecialistaAvaliacaoResponse(
    int Id,
    string Paciente,
    string Feedback,
    int Score,
    string Marcacao,
    DateTime CreatedAt)
{
    public static EspecialistaAvaliacaoResponse MapFromEntity(EspecialistaAvaliacao avaliacao)
        => avaliacao is null
            ? default!
            : new EspecialistaAvaliacaoResponse(
                avaliacao.Id,
                avaliacao.Paciente.Nome,
                avaliacao.Feedback,
                avaliacao.Score,
                avaliacao.Marcacao.Descricao,
                avaliacao.CreatedAt);

    public static IReadOnlyList<EspecialistaAvaliacaoResponse>? MapFromEntity(IEnumerable<EspecialistaAvaliacao> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}