﻿using RSF.AgendamentoConsultas.Shareable.Results;

namespace RSF.AgendamentoConsultas.Application.Features.Especialista.Responses;

public class EspecialistaResponse
{
    public int Id { get; set; }
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
    public decimal? Avaliacao { get; set; }
    public string ExperienciaProfissional { get; set; }
    public string FormacaoAcademica { get; set; }
    public string Genero { get; set; }
    public string Tratamento { get; set; }

    public IReadOnlyList<EspecialistaEspecialidadeResponse>? Especialidades { get; set; }
    public IReadOnlyList<EspecialistaConvenioMedicoResponse>? ConveniosMedicosAtendidos { get; set; }
    public IReadOnlyList<EspecialistaTagsResponse>? Marcacoes { get; set; }
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
            Tipo = entity.Tipo,
            Nome = entity.Nome,
            Licenca = entity.Licenca,
            Foto = entity.Foto!.Contains("https://boaconsulta") ? entity.Foto : default!,
            AgendaOnline = entity.AgendaOnline,
            PerfilVerificado = entity.PerfilVerificado,
            PermitirPergunta = entity.PermitirPergunta,
            TelemedicinaOnline = entity.TelemedicinaOnline,
            TelemedicinaAtivo = entity.TelemedicinaAtivo,
            TelemedicinaPreco = entity.TelemedicinaPreco,
            TelemedicinaPrecoNumber = entity.TelemedicinaPrecoNumber,
            Avaliacao = entity.Avaliacao,
            ExperienciaProfissional = entity.ExperienciaProfissional,
            FormacaoAcademica = entity.FormacaoAcademica,
            Genero = entity.Genero,
            Tratamento = entity.Tratamento,
            Especialidades = EspecialistaEspecialidadeResponse.MapFromEntity(entity.Especialidades?.Select(c => c.Especialidade)),
            ConveniosMedicosAtendidos = EspecialistaConvenioMedicoResponse.MapFromEntity(entity.ConveniosMedicosAtendidos?.Select(c => c.ConvenioMedico)),
            Marcacoes = EspecialistaTagsResponse.MapFromEntity(entity.Tags?.Select(c => c.Tag)),
            LocaisAtendimento = EspecialistaLocalAtendimentoResponse.MapFromEntity(entity.LocaisAtendimento),
            Avaliacoes = EspecialistaAvaliacaoResponse.MapFromEntity(entity.Avaliacoes),
            PerguntasRespostas = EspecialistaPerguntaResponse.MapFromEntity(entity.Perguntas)
        };
    }

    internal static PagedResult<EspecialistaResponse> MapFromEntityPaged(PagedResult<Domain.Entities.Especialista> pagedResult, int pageNumber, int pageSize)
    {
        if (pagedResult.Results is null || !pagedResult.Results.Any())
            pagedResult.Results = [];

        var lista = pagedResult.Results.Select(c => MapFromEntity(c));
        return new PagedResult<EspecialistaResponse>(lista, pagedResult.Total, pageNumber, pageSize);
        //{
        //    Results = lista,
        //    Total = pagedResult.Total,
        //    EndPage = pagedResult.EndPage,
        //    HasNextPage = pagedResult.HasNextPage,
        //    NextPage = pagedResult.NextPage
        //};
    }
}