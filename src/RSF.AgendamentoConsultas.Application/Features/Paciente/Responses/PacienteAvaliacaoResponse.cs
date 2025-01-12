using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;

public record PacienteAvaliacaoResponse(
    string Especialista,
    string Feedback,
    int Score,
    DateTime CreatedAt)
{
    public static PacienteAvaliacaoResponse MapFromEntity(EspecialistaAvaliacao avaliacao)
        => avaliacao is null
            ? default!
            : new PacienteAvaliacaoResponse(
                avaliacao.Especialista.Nome,
                avaliacao.Feedback,
                avaliacao.Score,
                avaliacao.CreatedAt);

    public static IReadOnlyList<PacienteAvaliacaoResponse>? MapFromEntity(IEnumerable<EspecialistaAvaliacao> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();
}