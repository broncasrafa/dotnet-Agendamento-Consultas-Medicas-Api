using RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Domain.Entities;

namespace RSF.AgendamentoConsultas.Application.Features.PerguntasRespostas.Responses;

public class PerguntaResponse
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Pergunta { get; set; }
    public DateTime CreatedAt { get; set; }

    public EspecialistaResponse Especialista { get; set; }
    public PacienteResponse Paciente { get; set; }


    public static PerguntaResponse MapFromEntity(EspecialistaPergunta entity)
        => entity is null ? default! : new PerguntaResponse
        {
            Id = entity.Id,
            Titulo = entity.Titulo,
            Pergunta = entity.Pergunta,
            CreatedAt = entity.CreatedAt,
            Especialista = entity.Especialista is null ? default! : new EspecialistaResponse { Id = entity.Especialista.EspecialistaId, Nome = entity.Especialista.Nome, },
            Paciente = entity.Paciente is null ? default! : new PacienteResponse { Id = entity.Paciente.PacienteId, Nome = entity.Paciente.Nome, }
        };
}