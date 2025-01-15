using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;

public class RespostaResponse
{
    public int Id { get; private set; }
    public string Resposta { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public PerguntaResponse Pergunta { get; set; }
    public EspecialistaResponse Especialista { get; set; }

    public static RespostaResponse MapFromEntity(PerguntaResposta entity)
     => entity is null ? default! : new RespostaResponse
     {
         Id = entity.PerguntaRespostaId,
         Resposta = entity.Resposta,
         CreatedAt = entity.CreatedAt,
         Pergunta = PerguntaResponse.MapFromEntity(entity.Pergunta),
         Especialista = EspecialistaResponse.MapFromEntity(entity.Especialista)
     };
}