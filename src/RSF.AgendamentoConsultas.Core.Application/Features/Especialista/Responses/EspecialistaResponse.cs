using RSF.AgendamentoConsultas.CrossCutting.Shareable.Results;

namespace RSF.AgendamentoConsultas.Core.Application.Features.Especialista.Responses;

public class EspecialistaResponse
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Tipo { get; set; }
    public string Nome { get; set; }
    public string Licenca { get; set; }
    public string Foto { get; set; }
    public bool? AgendaOnline { get; set; }
    public bool? PerfilVerificado { get; set; }
    public bool? PermitirPergunta { get; set; }
    public bool? TelemedicinaOnline { get; set; }
    public bool? TelemedicinaAtivo { get; set; }
    public string TelemedicinaPreco { get; set; }
    public decimal? TelemedicinaPrecoNumber { get; set; }
    public double? Avaliacao { get; set; }
    public string ExperienciaProfissional { get; set; }
    public string FormacaoAcademica { get; set; }
    public string Genero { get; set; }
    public string Tratamento { get; set; }

    public IReadOnlyList<EspecialistaEspecialidadeResponse>? Especialidades { get; set; }
    public IReadOnlyList<EspecialistaConvenioMedicoResponse>? ConveniosMedicosAtendidos { get; set; }
    public IReadOnlyList<EspecialistaLocalAtendimentoResponse>? LocaisAtendimento { get; set; }
    public IReadOnlyList<EspecialistaAvaliacaoResponse>? Avaliacoes { get; set; }
    public IReadOnlyList<EspecialistaPerguntaResponse>? PerguntasRespostas { get; set; }

    public EspecialistaResponse()
    {
    }

    public static EspecialistaResponse MapFromEntity(Domain.Entities.Especialista entity)
    {
        if (entity is null) return default!;

        return new EspecialistaResponse
        {
            Id = entity.EspecialistaId,
            UserId = entity.UserId,
            Tipo = entity.Tipo,
            Nome = entity.Nome,
            Licenca = entity.Licenca,
            Foto = entity.Foto ?? default!,
            AgendaOnline = entity.AgendaOnline,
            PerfilVerificado = entity.PerfilVerificado,
            PermitirPergunta = entity.PermitirPergunta,
            TelemedicinaOnline = entity.TelemedicinaOnline,
            TelemedicinaAtivo = entity.TelemedicinaAtivo,
            TelemedicinaPreco = entity.TelemedicinaPreco,
            TelemedicinaPrecoNumber = entity.TelemedicinaPrecoNumber,
            Avaliacao = entity.Avaliacoes is null || !entity.Avaliacoes.Any() ? null : entity.Avaliacoes.Average(c => c.Score),
            ExperienciaProfissional = entity.ExperienciaProfissional,
            FormacaoAcademica = entity.FormacaoAcademica,
            Genero = entity.Genero,
            Tratamento = entity.Tratamento,
            Especialidades = EspecialistaEspecialidadeResponse.MapFromEntity(entity.Especialidades?.Select(c => c.Especialidade)),
            ConveniosMedicosAtendidos = EspecialistaConvenioMedicoResponse.MapFromEntity(entity.ConveniosMedicosAtendidos?.Select(c => c.ConvenioMedico)),
            LocaisAtendimento = EspecialistaLocalAtendimentoResponse.MapFromEntity(entity.LocaisAtendimento),
            Avaliacoes = EspecialistaAvaliacaoResponse.MapFromEntity(entity.Avaliacoes),
            PerguntasRespostas = null
        };
    }
    public static IReadOnlyList<EspecialistaResponse>? MapFromEntity(IEnumerable<Domain.Entities.Especialista> collection)
        => collection is null || !collection.Any() ? default! : collection.Select(c => MapFromEntity(c)).ToList();

    internal static PagedResult<EspecialistaResponse> MapFromEntityPaged(PagedResult<Domain.Entities.Especialista> pagedResult, int pageNumber, int pageSize)
    {
        if (pagedResult.Results is null || !pagedResult.Results.Any())
            pagedResult.Results = [];

        var lista = pagedResult.Results.Select(c => MapFromEntity(c));
        return new PagedResult<EspecialistaResponse>(lista, pagedResult.Total, pageNumber, pageSize);
    }
}
