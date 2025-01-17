namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostasReacoes.Responses;

public class ReacaoRespostaResponse
{
    public int Id { get; set; }
    public int RespostaId { get; set; }
    public int PacienteId { get; set; }
    public string Reacao { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public static ReacaoRespostaResponse MapFromEntity(Domain.Entities.PerguntaRespostaReacao entity)
     => entity is null ? default! : new ReacaoRespostaResponse
     {
         Id = entity.PerguntaRespostaReacaoId,
         RespostaId = entity.RespostaId,
         PacienteId = entity.PacienteId,
         CreatedAt = entity.CreatedAt,
         Reacao = entity.Reacao.ToString(),
     };

    public static IReadOnlyList<ReacaoRespostaResponse> MapFromEntity(IEnumerable<Domain.Entities.PerguntaRespostaReacao> collection)
        => collection?.Count() == 0 ? default! : collection!.Select(MapFromEntity).ToList();
}

