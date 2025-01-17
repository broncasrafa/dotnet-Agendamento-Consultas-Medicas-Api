using RSF.AgendamentoConsultas.Application.Features.Especialidade.Responses;
using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Shareable.Enums;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;

public class RespostaResponse
{
    public int Id { get; private set; }
    public string Resposta { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int? Likes { get; set; }
    public int? Dislikes { get; set; }
    public PerguntaResponse Pergunta { get; set; }
    public EspecialistaResponse Especialista { get; set; }
    public EspecialidadeResponse Especialidade { get; set; }
    public PacienteResponse Paciente { get; set; }

    public static RespostaResponse MapFromEntity(Domain.Entities.PerguntaResposta entity)
     => entity is null ? default! : new RespostaResponse
     {
         Id = entity.PerguntaRespostaId,
         Resposta = entity.Resposta,
         CreatedAt = entity.CreatedAt,
         Pergunta = entity.Pergunta is null ? default! : new PerguntaResponse
         {
             Id = entity.Pergunta.PerguntaId,
             Pergunta = entity.Pergunta.Texto,
             CreatedAt = entity.Pergunta.CreatedAt
         },
         Especialista = entity.Especialista is null ? default! : new EspecialistaResponse
         {
             Id = entity.Especialista.EspecialistaId,
             Nome = entity.Especialista.Nome,
             Licenca = entity.Especialista.Licenca,
             Foto = entity.Especialista.Foto
         },
         Especialidade = entity.Pergunta!.Especialidade is null ? default! : new EspecialidadeResponse
         {
             Id = entity.Pergunta.Especialidade.EspecialidadeId,
             Nome = entity.Pergunta.Especialidade.NomePlural,
             Grupo = entity.Pergunta.Especialidade.EspecialidadeGrupo?.NomePlural!
         },
         Paciente = entity.Pergunta.Paciente is null ? default! : new PacienteResponse
         {
             Id = entity.Pergunta.Paciente.PacienteId,
             Nome = entity.Pergunta.Paciente.Nome,
             Email = entity.Pergunta.Paciente.Email,
         },
         Likes = entity.Reacoes is null ? null : entity.Reacoes.Where(c => c.Reacao.ToString().Equals(ETipoReacaoResposta.Like.ToString())).Count(),
         Dislikes = entity.Reacoes is null ? null : entity.Reacoes.Where(c => c.Reacao.ToString().Equals(ETipoReacaoResposta.Dislike.ToString())).Count()
     };

    public static IReadOnlyList<RespostaResponse> MapFromEntity(IEnumerable<Domain.Entities.PerguntaResposta> collection)
        => collection?.Count() == 0 ? default! : collection!.Select(MapFromEntity).ToList();
}