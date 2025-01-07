using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class Paciente
{
    public int PacienteId { get; set; }

    /// <summary>
    /// Tipo do paciente, podendo ser "Principal" ou "Dependente".
    /// </summary>
    public string Tipo { get; set; }
    /// <summary>
    /// Se for um paciente dependente, esse campo deve ser preenchido com o id do paciente principal.
    /// </summary>
    public int? PacientePrincipalId { get; set; }
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

    public ICollection<PacientePlanoMedico> PlanosMedicos { get; set; }
    public ICollection<EspecialistaAvaliacao> AvaliacoesFeitas { get; set; }
    public ICollection<EspecialistaPergunta> PerguntasRealizadas { get; set; }
    public ICollection<AgendamentoConsulta> AgendamentosRealizados { get; set; }

    protected Paciente() { }

    public Paciente(string tipo, int? pacientePrincipalId, string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string nomeSocial, decimal? peso, decimal? altura, bool telefoneVerificado, bool emailVerificado, bool termoUsoAceito)
    {
        Tipo = tipo;
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
        TelefoneVerificado = telefoneVerificado;
        EmailVerificado = emailVerificado;
        TermoUsoAceito = termoUsoAceito;
        CreatedAt = DateTime.UtcNow;

        Validate();
    }

    public void Update(string nome, string email, string telefone, string genero, string dataNascimento, string nomeSocial, decimal? peso, decimal? altura, bool telefoneVerificado, bool emailVerificado, bool termoUsoAceito)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        Genero = genero;
        DataNascimento = dataNascimento;
        NomeSocial = nomeSocial;
        Peso = peso;
        Altura = altura;
        TelefoneVerificado = telefoneVerificado;
        EmailVerificado = emailVerificado;
        TermoUsoAceito = termoUsoAceito;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }

    void Validate()
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
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_PACIENTES, value: Tipo, nameof(Tipo));
        DomainValidation.IdentifierGreaterThanZero(PacientePrincipalId, nameof(PacientePrincipalId));
    }
}