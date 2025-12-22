using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Entities;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Responses;

public class RespostaResponse
{
    public int Id { get; private set; }
    public string Resposta { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedAtFormatted { get; private set; }
    public int LikesCount { get; set; }
    public bool LikedByCurrentUser { get; set; }
    public EspecialistaResponse Especialista { get; set; }

    public static RespostaResponse MapFromEntity(PerguntaResposta entity, int? pacienteId = null)
    {
        if (entity is null) return default!;

        var totalLikes = entity.Reacoes?.Where(c => c.Reacao.ToString().Equals(ETipoReacaoResposta.Like.ToString()))?.Count() ?? 0;
        var likedByCurrentUser = pacienteId.HasValue && entity.Reacoes?.Any(r => r.PacienteId == pacienteId.Value) == true;

        return new RespostaResponse
        {
            Id = entity.PerguntaRespostaId,
            Resposta = entity.Resposta,
            CreatedAt = entity.CreatedAt,
            CreatedAtFormatted = Utilitarios.DataFormatadaExtenso(entity.CreatedAt),
            LikesCount = totalLikes,
            LikedByCurrentUser = likedByCurrentUser,       
            Especialista = entity.Especialista is null ? default! : new EspecialistaResponse
            {
                Id = entity.Especialista.EspecialistaId,
                Nome = entity.Especialista.Nome,
                Licenca = entity.Especialista.Licenca,
                Foto = entity.Especialista.Foto,
                Avaliacao = entity.Especialista.Avaliacoes is null || !entity.Especialista.Avaliacoes.Any() ? null : entity.Especialista.Avaliacoes.Average(c => c.Score),
                Especialidades = EspecialistaEspecialidadeResponse.MapFromEntity(entity.Especialista.Especialidades?.Select(c => c.Especialidade)),
            }
        };
    }

    public static IReadOnlyList<RespostaResponse> MapFromEntity(IEnumerable<PerguntaResposta> collection, int? pacienteId = null)
        => collection?.Count() == 0 ? default! : collection!.Select(c => MapFromEntity(c, pacienteId)).ToList();
}