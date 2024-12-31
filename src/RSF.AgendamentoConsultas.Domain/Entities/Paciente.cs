using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class Paciente
{
    public int PacienteId { get; private set; }

    /// <summary>
    /// Tipo do paciente, podendo ser "Principal" ou "Dependente".
    /// </summary>
    public string Tipo { get; private set; }
    /// <summary>
    /// Se for um paciente dependente, esse campo deve ser preenchido com o id do paciente principal.
    /// </summary>
    public int? PacientePrincipalId { get; private set; }
    public string Nome { get; private set; }
    public string CPF { get; private set; }
    public string Email { get; private set; }
    public string Telefone { get; private set; }
    public string Genero { get; private set; } = "Não informado";
    public string DataNascimento { get; private set; }
    public string NomeSocial { get; private set; }
    public decimal? Peso { get; private set; }
    public decimal? Altura { get; private set; }
    public bool TelefoneVerificado { get; private set; }
    public bool EmailVerificado { get; private set; }
    public bool TermoUsoAceito { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public ICollection<PacientePlanoMedico> PlanosMedicos { get; set; }
    public ICollection<EspecialistaAvaliacao> AvaliacoesFeitas { get; set; }
    public ICollection<EspecialistaPergunta> PerguntasRealizadas { get; set; }
    public ICollection<AgendamentoConsulta> AgendamentosRealizados { get; set; }

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
        Tipo = tipo;
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

    private void Validate()
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