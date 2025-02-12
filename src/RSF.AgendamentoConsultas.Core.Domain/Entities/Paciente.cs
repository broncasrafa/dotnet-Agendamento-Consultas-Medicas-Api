using RSF.AgendamentoConsultas.Core.Domain.Validation;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class Paciente
{
    public int PacienteId { get; set; }
    public string UserId { get; private set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Genero { get; set; } = "Não informado";
    public string DataNascimento { get; set; }
    public string NomeSocial { get; set; }
    public decimal? Peso { get; set; }
    public decimal? Altura { get; set; }
    public bool? TelefoneVerificado { get; set; }
    public bool? EmailVerificado { get; set; }
    public bool? TermoUsoAceito { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public bool? Ativo { get; set; }

    public ICollection<PacienteDependente> Dependentes { get; set; }
    public ICollection<PacientePlanoMedico> PlanosMedicos { get; set; }
    public ICollection<EspecialistaAvaliacao> AvaliacoesFeitas { get; set; }
    public ICollection<Pergunta> PerguntasRealizadas { get; set; }
    public ICollection<AgendamentoConsulta> AgendamentosRealizados { get; set; }

    protected Paciente() { }

    public Paciente(int pacienteId, string userId, string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string nomeSocial = null, decimal? peso = null, decimal? altura = null, bool? telefoneVerificado = null, bool? emailVerificado = null, bool? termoUsoAceito = null)
    {
        DomainValidation.IdentifierGreaterThanZero(pacienteId, nameof(PacienteId));

        PacienteId = pacienteId;
        UserId = userId;
        Nome = nome;
        CPF = cpf.RemoverFormatacaoSomenteNumeros();
        Email = email;
        Telefone = telefone.RemoverFormatacaoSomenteNumeros();
        Genero = genero;
        DataNascimento = dataNascimento;
        NomeSocial = nomeSocial;
        Peso = peso;
        Altura = altura;
        TelefoneVerificado = telefoneVerificado ?? false;
        EmailVerificado = emailVerificado ?? false;
        TermoUsoAceito = termoUsoAceito ?? false;
        CreatedAt = DateTime.Now;
        Ativo = true;

        Validate();
    }


    public Paciente(string userId, string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string nomeSocial = null, decimal? peso = null, decimal? altura = null, bool? telefoneVerificado = null, bool? emailVerificado = null, bool? termoUsoAceito = null)
    {
        UserId = userId;
        Nome = nome;
        CPF = cpf.RemoverFormatacaoSomenteNumeros();
        Email = email;
        Telefone = telefone.RemoverFormatacaoSomenteNumeros();
        Genero = genero;
        DataNascimento = dataNascimento;
        NomeSocial = nomeSocial;
        Peso = peso;
        Altura = altura;
        TelefoneVerificado = telefoneVerificado ?? false;
        EmailVerificado = emailVerificado ?? false;
        TermoUsoAceito = termoUsoAceito ?? false;
        CreatedAt = DateTime.Now;
        Ativo = true;

        Validate();
    }

    public void Update(string nome, string email, string telefone, string genero, string dataNascimento, string nomeSocial = null, decimal? peso = null, decimal? altura = null, bool? telefoneVerificado = null, bool? emailVerificado = null, bool? termoUsoAceito = null)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone.RemoverFormatacaoSomenteNumeros();
        Genero = genero;
        DataNascimento = dataNascimento;
        NomeSocial = nomeSocial;
        Peso = peso;
        Altura = altura;
        TelefoneVerificado = telefoneVerificado ?? false;
        EmailVerificado = emailVerificado ?? false;
        TermoUsoAceito = termoUsoAceito ?? false;
        UpdatedAt = DateTime.Now;

        Validate();
    }
    
    public void ChangeStatus(bool status) => Ativo = status;

    void Validate()
    {
        DomainValidation.NotNullOrEmpty(UserId, nameof(UserId));

        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));

        DomainValidation.NotNullOrEmpty(CPF, nameof(CPF));
        DomainValidation.PossibleValidCpf(CPF);

        DomainValidation.NotNullOrEmpty(Email, nameof(Email));
        DomainValidation.PossibleValidEmailAddress(Email, nameof(Email));

        DomainValidation.NotNullOrEmpty(Telefone, nameof(Telefone));
        DomainValidation.PossibleValidPhoneNumber(Telefone, nameof(Telefone));

        DomainValidation.NotNullOrEmpty(Genero, nameof(Genero));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_GENEROS, value: Genero, nameof(Genero));

        DomainValidation.NotNullOrEmpty(DataNascimento, nameof(DataNascimento));
        DomainValidation.PossibleValidDate(DataNascimento, permitirSomenteDatasFuturas: false, nameof(DataNascimento));
    }
}