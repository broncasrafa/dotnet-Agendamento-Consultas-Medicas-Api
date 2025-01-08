using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class PacienteDependente
{
    public int DependenteId { get; set; }
    public int PacientePrincipalId { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Genero { get; set; } = "Não informado";
    public string DataNascimento { get; set; }
    public string NomeSocial { get; set; }
    public decimal? Peso { get; set; }
    public decimal? Altura { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Paciente Paciente { get; set; }
    public ICollection<PacienteDependentePlanoMedico> PlanosMedicos { get; set; }

    protected PacienteDependente() { }

    public PacienteDependente(int pacientePrincipalId, string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string nomeSocial = null, decimal? peso = null, decimal? altura = null)
    {
        PacientePrincipalId = pacientePrincipalId;
        Nome = nome;
        CPF = cpf;
        Email = email;
        Telefone = telefone;
        Genero = genero;
        DataNascimento = dataNascimento;
        NomeSocial = nomeSocial;
        Peso = peso;
        Altura = altura;
        CreatedAt = DateTime.UtcNow;

        Validate();
    }

    public void Update(string nome, string email, string telefone, string genero, string dataNascimento, string nomeSocial = null, decimal? peso = null, decimal? altura = null)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Genero = genero;
        DataNascimento = dataNascimento;
        NomeSocial = nomeSocial;
        Peso = peso;
        Altura = altura;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }

    void Validate()
    {
        DomainValidation.IdentifierGreaterThanZero(PacientePrincipalId, nameof(PacientePrincipalId));
        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
        DomainValidation.NotNullOrEmpty(CPF, nameof(CPF));
        DomainValidation.NotNullOrEmpty(Email, nameof(Email));
        DomainValidation.NotNullOrEmpty(Telefone, nameof(Telefone));
        DomainValidation.NotNullOrEmpty(Genero, nameof(Genero));
        DomainValidation.NotNullOrEmpty(DataNascimento, nameof(DataNascimento));
        DomainValidation.PossibleValidDate(DataNascimento, permitirSomenteDatasFuturas: false, nameof(DataNascimento));
        DomainValidation.PossibleValidPhoneNumber(Telefone, nameof(Telefone));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_GENEROS, value: Genero, nameof(Genero));
    }
}