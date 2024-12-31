using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class PacienteDependente
{
    public int PacienteDependenteId { get; private set; }
    public int PacienteId { get; private set; }
    public string Nome { get; private set; }
    public string CPF { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public string Genero { get; private set; } = "Não informado";
    public string DataNascimento { get; private set; }
    public string NomeSocial { get; private set; }
    public decimal? Peso { get; private set; }
    public decimal? Altura { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Paciente PacientePrincipal { get; set; }

    public PacienteDependente(int pacienteId, string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string nomeSocial, decimal? peso, decimal? altura)
    {
        PacienteId = pacienteId;
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

    public void Update(string nome, string email, string telefone, string genero, string dataNascimento, string nomeSocial, decimal? peso, decimal? altura)
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

    private void Validate()
    {
        DomainValidation.IdentifierGreaterThanZero(PacienteId, nameof(PacienteId));
        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
        DomainValidation.NotNullOrEmpty(Telefone, nameof(Telefone));
        DomainValidation.NotNullOrEmpty(Genero, nameof(Genero));
        DomainValidation.NotNullOrEmpty(DataNascimento, nameof(DataNascimento));
        DomainValidation.PossibleValidDate(DataNascimento, permitirSomenteDatasFuturas: false, nameof(DataNascimento));
        DomainValidation.PossibleValidPhoneNumber(Telefone, nameof(Telefone));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_GENEROS, value: Genero, nameof(Genero));
    }
}