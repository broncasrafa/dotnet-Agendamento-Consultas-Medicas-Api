using RSF.AgendamentoConsultas.Application.Features.Especialidade.Responses;
using RSF.AgendamentoConsultas.Application.Features.Paciente.Responses;

namespace RSF.AgendamentoConsultas.Application.Features.Pergunta.Responses;

public class PerguntaResponse
{
    public int Id { get; set; }
    public string Pergunta { get; set; }
    public DateTime CreatedAt { get; set; }

    public EspecialidadeResponse Especialidade { get; set; }
    public PacienteResponse Paciente { get; set; }


    public static PerguntaResponse MapFromEntity(Domain.Entities.Pergunta entity)
        => entity is null ? default! : new PerguntaResponse
        {
            Id = entity.PerguntaId,
            Pergunta = entity.Texto,
            CreatedAt = entity.CreatedAt,
            Especialidade = entity.Especialidade is null ? default! : new EspecialidadeResponse 
            { 
                Id = entity.Especialidade.EspecialidadeId, 
                Nome = entity.Especialidade.Nome, 
                Grupo = entity.Especialidade.EspecialidadeGrupo?.NomePlural!
            },
            Paciente = entity.Paciente is null ? default! : new PacienteResponse 
            { 
                Id = entity.Paciente.PacienteId, 
                Nome = entity.Paciente.Nome, 
            }
        };

    public static IReadOnlyList<PerguntaResponse>? MapFromEntity(IEnumerable<Domain.Entities.Pergunta> convenios)
        => convenios is null || !convenios.Any() ? null : convenios.Select(MapFromEntity).ToList();
}