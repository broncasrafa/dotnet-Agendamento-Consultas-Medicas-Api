using RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.Paciente.Responses;
using RSF.AgendamentoConsultas.Core.Domain.Entities;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Avaliacao.Responses;

public class AvaliacaoResponse
{
    public int Id { get; private set; }
    public string Feedback { get; private set; }
    public int Score { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public EspecialistaResponse Especialista { get; set; }
    public PacienteResponse Paciente { get; set; }


    public static AvaliacaoResponse MapFromEntity(EspecialistaAvaliacao avaliacao)
        => avaliacao is null ? default! : new AvaliacaoResponse
        {
            Id = avaliacao.Id,
            Feedback = avaliacao.Feedback,
            Score = avaliacao.Score,
            CreatedAt = avaliacao.CreatedAt,
            Especialista = avaliacao.Especialista is null ? default! : new EspecialistaResponse { Id = avaliacao.Especialista.EspecialistaId, Nome = avaliacao.Especialista.Nome, },
            Paciente = avaliacao.Paciente is null ? default! : new PacienteResponse { Id = avaliacao.Paciente.PacienteId, Nome = avaliacao.Paciente.Nome, }
        };
}