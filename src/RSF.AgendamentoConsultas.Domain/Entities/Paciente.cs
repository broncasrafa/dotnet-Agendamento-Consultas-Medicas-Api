using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class Paciente
{
    public int PacienteId { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Genero { get; set; } = "Não informado";
    public string DataNascimento { get; set; }
    public string NomeSocial { get; set; }
    public decimal? Peso { get; set; }
    public decimal? Altura { get; set; }
    public bool TelefoneVerificado { get; set; }
    public bool EmailVerificado { get; set; }
    public bool TermoUsoAceito { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string Password { get; private set; }
    public bool IsActive { get; private set; }

    public ICollection<PacienteDependente> Dependentes { get; set; }
    public ICollection<PacientePlanoMedico> PlanosMedicos { get; set; }
    public ICollection<EspecialistaAvaliacao> AvaliacoesFeitas { get; set; }
    public ICollection<Pergunta> PerguntasRealizadas { get; set; }
    public ICollection<AgendamentoConsulta> AgendamentosRealizados { get; set; }

    protected Paciente() { }

    public Paciente(string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string nomeSocial = null, decimal? peso = null, decimal? altura = null, bool? telefoneVerificado = null, bool? emailVerificado = null, bool? termoUsoAceito = null)
    {
        Nome = nome;
        CPF = cpf;
        Email = email;
        Telefone = telefone;
        Genero = genero;
        DataNascimento = dataNascimento;
        NomeSocial = nomeSocial;
        Peso = peso;
        Altura = altura;
        TelefoneVerificado = telefoneVerificado ?? false;
        EmailVerificado = emailVerificado ?? false;
        TermoUsoAceito = termoUsoAceito ?? false;
        CreatedAt = DateTime.UtcNow;

        Validate();
    }

    public void Update(string nome, string email, string telefone, string genero, string dataNascimento, string nomeSocial = null, decimal? peso = null, decimal? altura = null, bool? telefoneVerificado = null, bool? emailVerificado = null, bool? termoUsoAceito = null)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Genero = genero;
        DataNascimento = dataNascimento;
        NomeSocial = nomeSocial;
        Peso = peso;
        Altura = altura;
        TelefoneVerificado = telefoneVerificado ?? false;
        EmailVerificado = emailVerificado ?? false;
        TermoUsoAceito = termoUsoAceito ?? false;
        UpdatedAt = DateTime.UtcNow;

        Validate(validatePassword: false);
    }

    public void SetPassword(string passwordHash) => Password = passwordHash;
    public void ChangeStatus(bool status) => IsActive = status;

    void Validate(bool validatePassword = true)
    {
        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
        DomainValidation.NotNullOrEmpty(CPF, nameof(CPF));
        DomainValidation.NotNullOrEmpty(Email, nameof(Email));
        DomainValidation.NotNullOrEmpty(Telefone, nameof(Telefone));
        DomainValidation.NotNullOrEmpty(Genero, nameof(Genero));
        DomainValidation.NotNullOrEmpty(DataNascimento, nameof(DataNascimento));
        DomainValidation.PossibleValidDate(DataNascimento, permitirSomenteDatasFuturas: false, nameof(DataNascimento));
        DomainValidation.PossibleValidPhoneNumber(Telefone, nameof(Telefone));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_GENEROS, value: Genero, nameof(Genero));
        if(validatePassword) DomainValidation.PossibleValidPassword(Password);
    }
}