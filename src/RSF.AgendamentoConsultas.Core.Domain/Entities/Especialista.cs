using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;
using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class Especialista
{
    public int EspecialistaId { get; set; }
    public string UserId { get; set; }
    public string Tipo { get; set; }
    public string Code { get; set; }
    public string CodeId { get; set; }
    public string EspecCodeId { get; set; }
    public string Nome { get; set; }
    public string Licenca { get; set; }    
    public string Email { get; set; }
    public string Foto { get; set; }
    public bool? Ativo { get; set; }
    public bool? AgendaOnline { get; set; }
    public bool? PerfilVerificado { get; set; }
    public bool? PermitirPergunta { get; set; }
    public bool? TelemedicinaOnline { get; set; }
    public bool? TelemedicinaAtivo { get; set; }
    public string TelemedicinaPreco { get; private set; }
    public decimal? TelemedicinaPrecoNumber { get; set; }
    public string ExperienciaProfissional { get; set; }
    public string FormacaoAcademica { get; set; }
    public string Genero { get; set; }
    public string Tratamento { get; private set; }

    public ICollection<EspecialistaEspecialidade> Especialidades { get; set; }
    public ICollection<EspecialistaConvenioMedico> ConveniosMedicosAtendidos { get; set; }
    public ICollection<EspecialistaLocalAtendimento> LocaisAtendimento { get; set; }
    public ICollection<EspecialistaAvaliacao> Avaliacoes { get; set; }
    public ICollection<PerguntaResposta> Respostas { get; set; }
    public ICollection<AgendamentoConsulta> ConsultasAtendidas { get; set; }


    protected Especialista()
    {        
    }

    public Especialista(string userId, string nome, string licenca, string email, string genero,
        string? tipo = null,
        string? foto = null, 
        string? sharedUrl = null, 
        bool? agendaOnline = null,
        bool? telemedicinaOnline = null, 
        bool? telemedicinaAtivo = null,
        decimal? telemedicinaPrecoNumber = null,
        string? experienciaProfissional = null, 
        string? formacaoAcademica = null)
    {
        UserId = userId;
        Tipo = tipo;
        Code = Utilitarios.GenerateSlugifyString(nome);
        CodeId = Guid.NewGuid().ToString();
        EspecCodeId = $"{Code}-{CodeId}";
        Nome = nome;
        Licenca = licenca;
        Email = email;
        Foto = foto;
        Ativo = true;
        AgendaOnline = agendaOnline;
        PerfilVerificado = true;
        PermitirPergunta = true;
        TelemedicinaOnline = telemedicinaOnline;
        TelemedicinaAtivo = telemedicinaAtivo;
        TelemedicinaPrecoNumber = telemedicinaPrecoNumber;
        TelemedicinaPreco = Utilitarios.ConverterMoedaParaExtenso(telemedicinaPrecoNumber);
        ExperienciaProfissional = experienciaProfissional;
        FormacaoAcademica = formacaoAcademica;
        Genero = (genero is null || genero == "Não informado") ? "Não informado" : genero;
        Tratamento = (genero == "Não informado") ? "Não informado" : (Genero == "Masculino" ? "Dr." : "Dra.");

        Validate();
    }

    public void Update(
        string nome, 
        string? tipo = null,
        string? foto = null,
        bool? agendaOnline = null,
        bool? telemedicinaOnline = null,
        bool? telemedicinaAtivo = null,
        decimal? telemedicinaPrecoNumber = null,
        string? experienciaProfissional = null,
        string? formacaoAcademica = null)
    {
        Tipo = tipo;
        Code = Utilitarios.GenerateSlugifyString(nome);
        EspecCodeId = $"{Code}-{CodeId}";
        Nome = nome;
        Foto = foto;
        AgendaOnline = agendaOnline;
        TelemedicinaOnline = telemedicinaOnline;
        TelemedicinaAtivo = telemedicinaAtivo;
        TelemedicinaPrecoNumber = telemedicinaPrecoNumber;
        TelemedicinaPreco = Utilitarios.ConverterMoedaParaExtenso(telemedicinaPrecoNumber);
        ExperienciaProfissional = experienciaProfissional;
        FormacaoAcademica = formacaoAcademica;
        Tratamento = (Genero is null || Genero == "Não informado") ? "Não informado" : (Genero == "Masculino" ? "Dr." : "Dra.");

        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(UserId, nameof(UserId));
        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
        DomainValidation.NotNullOrEmpty(Licenca, nameof(Licenca));
        DomainValidation.PossibleValidEmailAddress(Email, nameof(Email));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_GENEROS, value: Genero, nameof(Genero));
        if (!string.IsNullOrWhiteSpace(Tipo)) DomainValidation.PossiblesValidTypes(TypeValids.VALID_CATEGORIAS, value: Tipo, nameof(Tipo));
        if (!string.IsNullOrWhiteSpace(Tratamento)) DomainValidation.PossiblesValidTypes(TypeValids.VALID_TRATAMENTOS, value: Tratamento, nameof(Tratamento));
    }
}