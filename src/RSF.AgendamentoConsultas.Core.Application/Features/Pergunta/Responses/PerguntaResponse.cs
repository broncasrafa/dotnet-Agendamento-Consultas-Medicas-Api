using RSF.AgendamentoConsultas.Core.Application.Features.Especialidade.Responses;
using RSF.AgendamentoConsultas.Core.Application.Features.PerguntasRespostas.Responses;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Pergunta.Responses;

public class PerguntaResponse
{
    public int Id { get; set; }
    public string Pergunta { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedAtFormatted { get; private set; }
    public string PacienteNome { get; private set; }
    public int TotalRespostas { get; private set; }
    public EspecialidadeResponse Especialidade { get; set; }
    public IReadOnlyList<RespostaResponse> Respostas { get; private set; }


    public static PerguntaResponse MapFromEntity(Domain.Entities.Pergunta entity)
        => entity is null ? default! : new PerguntaResponse
        {
            Id = entity.PerguntaId,
            Pergunta = entity.Texto,
            CreatedAt = entity.CreatedAt,
            CreatedAtFormatted = Utilitarios.DataFormatadaExtenso(entity.CreatedAt),
            PacienteNome = entity.Paciente?.Nome!,
            TotalRespostas = entity.Respostas?.Count ?? 0,
            Especialidade = entity.Especialidade is null ? default! : new EspecialidadeResponse 
            { 
                Id = entity.Especialidade.EspecialidadeId, 
                Nome = entity.Especialidade.NomePlural,
                Grupo = entity.Especialidade.EspecialidadeGrupo?.NomePlural!
            },
            Respostas = RespostaResponse.MapFromEntity(entity.Respostas)
        };

    public static IReadOnlyList<PerguntaResponse>? MapFromEntity(IEnumerable<Domain.Entities.Pergunta> convenios)
        => convenios is null || !convenios.Any() ? null : convenios.Select(MapFromEntity).ToList();
}