using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Enums;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;

namespace RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Responses;

public class RespostaResponse
{
    public int Id { get; private set; }
    public string Resposta { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedAtFormatted { get; private set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }    
    public EspecialistaResponse Especialista { get; set; }
    public IReadOnlyList<PacienteLikeDislikeResponse> PacientesLikeDislike { get; set; }

    public static RespostaResponse MapFromEntity(Domain.Entities.PerguntaResposta entity)
     => entity is null ? default! : new RespostaResponse
     {
         Id = entity.PerguntaRespostaId,
         Resposta = entity.Resposta,
         CreatedAt = entity.CreatedAt,
         CreatedAtFormatted = Utilitarios.DataFormatadaExtenso(entity.CreatedAt),
         Likes = entity.Reacoes is null ? 0 : entity.Reacoes.Where(c => c.Reacao.ToString().Equals(ETipoReacaoResposta.Like.ToString())).Count(),
         Dislikes = entity.Reacoes is null ? 0 : entity.Reacoes.Where(c => c.Reacao.ToString().Equals(ETipoReacaoResposta.Dislike.ToString())).Count(),         
         Especialista = entity.Especialista is null ? default! : new EspecialistaResponse
         {
             Id = entity.Especialista.EspecialistaId,
             Nome = entity.Especialista.Nome,
             Licenca = entity.Especialista.Licenca,
             Foto = entity.Especialista.Foto,
             Avaliacao = entity.Especialista.Avaliacoes is null || !entity.Especialista.Avaliacoes.Any() ? null : entity.Especialista.Avaliacoes.Average(c => c.Score),
             Especialidades = EspecialistaEspecialidadeResponse.MapFromEntity(entity.Especialista.Especialidades?.Select(c => c.Especialidade)),
         },
         PacientesLikeDislike = entity.Reacoes is null ? default! : entity.Reacoes.Select(reacao => new PacienteLikeDislikeResponse
         {
             Pacienteid = reacao.PacienteId,
             Nome = reacao.Paciente.Nome,
             Reacao = reacao.Reacao.ToString()
         }).ToList()
     };

    public static IReadOnlyList<RespostaResponse> MapFromEntity(IEnumerable<Domain.Entities.PerguntaResposta> collection)
        => collection?.Count() == 0 ? default! : collection!.Select(MapFromEntity).ToList();
}