﻿using RSF.AgendamentoConsultas.Application.Features.Pergunta.Responses;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;

public class RespostaResponse
{
    public int Id { get; private set; }
    public string Resposta { get; private set; }
    public int? Likes { get; private set; }
    public int? Dislikes { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public PerguntaResponse Pergunta { get; set; }

    public static RespostaResponse MapFromEntity(PerguntaResposta entity)
     => entity is null ? default! : new RespostaResponse
     {
         Id = entity.PerguntaRespostaId,
         Resposta = entity.Resposta,
         //Likes = entity.Likes,
         //Dislikes = entity.Dislikes,
         CreatedAt = entity.CreatedAt,
         Pergunta = entity.Pergunta is null ? default! : new PerguntaResponse
         {
             Id = entity.Pergunta.PerguntaId,
             Pergunta = entity.Pergunta.Texto
         }
     };
}