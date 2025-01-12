using RSF.AgendamentoConsultas.Shareable.Helpers;
using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class Especialista
{
    public int EspecialistaId { get; private set; }
    public string Tipo { get; private set; }
    public string Code { get; private set; }
    public string CodeId { get; private set; }
    public string EspecCodeId { get; private set; }
    public string Nome { get; private set; }
    public string Licenca { get; private set; }
    public string Foto { get; private set; }
    public string SharedUrl { get; private set; }
    public bool? Ativo { get; private set; }
    public bool? AgendaOnline { get; private set; }
    public bool? PerfilVerificado { get; private set; }
    public bool? PermitirPergunta { get; private set; }
    public bool? TelemedicinaOnline { get; private set; }
    public bool? TelemedicinaAtivo { get; private set; }
    public string TelemedicinaPreco { get; private set; }
    public decimal? TelemedicinaPrecoNumber { get; private set; }
    public decimal? Avaliacao { get; private set; }
    public string ExperienciaProfissional { get; private set; }
    public string FormacaoAcademica { get; private set; }
    public string Genero { get; private set; }
    public string Tratamento { get; private set; }

    public ICollection<EspecialistaEspecialidade> Especialidades { get; set; }
    public ICollection<EspecialistaConvenioMedico> ConveniosMedicosAtendidos { get; set; }
    public ICollection<EspecialistaLocalAtendimento> LocaisAtendimento { get; set; }
    public ICollection<EspecialistaTags> Tags { get; set; }
    public ICollection<EspecialistaAvaliacao> Avaliacoes { get; set; }
    //public ICollection<Pergunta> Perguntas { get; set; }
    public ICollection<PerguntaResposta> Respostas { get; set; }
    

    protected Especialista()
    {        
    }

    public Especialista(string tipo, string nome, string licenca, string foto, string sharedUrl, bool? agendaOnline, bool? perfilVerificado, bool? permitirPergunta, bool? telemedicinaOnline, bool? telemedicinaAtivo, string telemedicinaPreco, decimal? telemedicinaPrecoNumber, decimal? avaliacao, string experienciaProfissional, string formacaoAcademica, string genero, string tratamento)
    {
        Tipo = tipo;
        Code = Utilitarios.GenerateSlugifyString(nome);
        CodeId = Guid.NewGuid().ToString();
        EspecCodeId = $"{Code}-{CodeId}";
        Nome = nome;
        Licenca = licenca;
        Foto = foto;
        SharedUrl = sharedUrl;
        Ativo = true;
        AgendaOnline = agendaOnline;
        PerfilVerificado = perfilVerificado;
        PermitirPergunta = permitirPergunta;
        TelemedicinaOnline = telemedicinaOnline;
        TelemedicinaAtivo = telemedicinaAtivo;
        TelemedicinaPreco = telemedicinaPreco;
        TelemedicinaPrecoNumber = telemedicinaPrecoNumber;
        Avaliacao = avaliacao;
        ExperienciaProfissional = experienciaProfissional;
        FormacaoAcademica = formacaoAcademica;
        Genero = genero;
        Tratamento = tratamento;

        Validate();
    }

    public void Update(string tipo, string nome, string licenca, string foto, string sharedUrl, bool? agendaOnline, bool? perfilVerificado, bool? permitirPergunta, bool? telemedicinaOnline, bool? telemedicinaAtivo, string telemedicinaPreco, decimal? telemedicinaPrecoNumber, decimal? avaliacao, string experienciaProfissional, string formacaoAcademica, string genero, string tratamento)
    {
        Tipo = tipo;
        Code = Utilitarios.GenerateSlugifyString(nome);
        EspecCodeId = $"{Code}-{CodeId}";
        Nome = nome;
        Licenca = licenca;
        Foto = foto;
        SharedUrl = sharedUrl;
        AgendaOnline = agendaOnline;
        PerfilVerificado = perfilVerificado;
        PermitirPergunta = permitirPergunta;
        TelemedicinaOnline = telemedicinaOnline;
        TelemedicinaAtivo = telemedicinaAtivo;
        TelemedicinaPreco = telemedicinaPreco;
        TelemedicinaPrecoNumber = telemedicinaPrecoNumber;
        Avaliacao = avaliacao;
        ExperienciaProfissional = experienciaProfissional;
        FormacaoAcademica = formacaoAcademica;
        Genero = genero;
        Tratamento = tratamento;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
        DomainValidation.NotNullOrEmpty(Licenca, nameof(Licenca));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_CATEGORIAS, value: Tipo, nameof(Nome));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_GENEROS, value: Genero, nameof(Genero));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_TRATAMENTOS, value: Tratamento, nameof(Tratamento));
    }
}