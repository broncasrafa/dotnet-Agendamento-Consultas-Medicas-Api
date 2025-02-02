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
        Genero = (string.IsNullOrWhiteSpace(genero) || genero == "Não informado") ? "Não informado" : genero;
        Tratamento = (genero == "Não informado") ? "Não informado" : (Genero == "Masculino" ? "Dr." : "Dra.");
        Foto = SetFoto(foto);
        Ativo = true;
        AgendaOnline = agendaOnline ?? false;
        PerfilVerificado = true;
        PermitirPergunta = true;
        TelemedicinaOnline = telemedicinaOnline ?? false;
        TelemedicinaAtivo = telemedicinaAtivo ?? false;
        TelemedicinaPrecoNumber = telemedicinaPrecoNumber;
        TelemedicinaPreco = Utilitarios.ConverterMoedaParaExtenso(telemedicinaPrecoNumber);
        ExperienciaProfissional = experienciaProfissional;
        FormacaoAcademica = formacaoAcademica;

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
        Foto = SetFoto(foto);
        AgendaOnline = agendaOnline;
        TelemedicinaOnline = telemedicinaOnline;
        TelemedicinaAtivo = telemedicinaAtivo;
        TelemedicinaPrecoNumber = telemedicinaPrecoNumber;
        TelemedicinaPreco = Utilitarios.ConverterMoedaParaExtenso(telemedicinaPrecoNumber);
        ExperienciaProfissional = experienciaProfissional;
        FormacaoAcademica = formacaoAcademica;
        
        Validate();
    }

    public void AddNovaEspecialidade(Especialidade especialidade, string tipoEspecialidade)
    {
        if (Especialidades is null)
            Especialidades = new List<EspecialistaEspecialidade>();

        Especialidades.Add(new EspecialistaEspecialidade(especialidade.EspecialidadeId, tipoEspecialidade));
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

    private string SetFoto(string foto)
    {
        if (!string.IsNullOrWhiteSpace(foto))
            return foto;

        if (Genero == "Não informado")
            return "https://static.vecteezy.com/system/resources/previews/000/288/638/large_2x/broker-vector-icon.jpg";

        return (Genero == "Masculino") 
            ? "https://static.vecteezy.com/system/resources/previews/045/711/185/large_2x/male-profile-picture-placeholder-for-social-media-forum-dating-site-chat-operator-design-social-profile-template-default-avatar-icon-flat-style-free-vector.jpg"
            : "https://static.vecteezy.com/system/resources/previews/042/332/098/large_2x/default-avatar-profile-icon-grey-photo-placeholder-female-no-photo-images-for-unfilled-user-profile-greyscale-illustration-for-socail-media-web-vector.jpg";
    }
}