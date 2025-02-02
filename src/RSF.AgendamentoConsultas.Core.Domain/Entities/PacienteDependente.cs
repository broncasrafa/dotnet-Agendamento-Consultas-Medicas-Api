using RSF.AgendamentoConsultas.Core.Domain.Validation;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

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

    public bool? Ativo { get; set; }

    public Paciente Paciente { get; set; }
    public ICollection<PacienteDependentePlanoMedico> PlanosMedicos { get; set; }
    public ICollection<AgendamentoConsulta> AgendamentosRealizados { get; set; }

    protected PacienteDependente() { }

    public PacienteDependente(int pacientePrincipalId, string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string nomeSocial = null, decimal? peso = null, decimal? altura = null)
    {
        PacientePrincipalId = pacientePrincipalId;
        Nome = nome;
        CPF = cpf.RemoverFormatacaoSomenteNumeros();
        Email = email;
        Telefone = telefone.RemoverFormatacaoSomenteNumeros();
        Genero = genero;
        DataNascimento = dataNascimento;
        NomeSocial = nomeSocial;
        Peso = peso;
        Altura = altura;
        CreatedAt = DateTime.Now;
        Ativo = true;

        Validate();
    }

    public void Update(string nome, string cpf, string email, string telefone, string genero, string dataNascimento, string nomeSocial = null, decimal? peso = null, decimal? altura = null)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone.RemoverFormatacaoSomenteNumeros();
        CPF = cpf.RemoverFormatacaoSomenteNumeros();
        Genero = genero;
        DataNascimento = dataNascimento;
        NomeSocial = nomeSocial;
        Peso = peso;
        Altura = altura;
        UpdatedAt = DateTime.Now;
        Paciente = null;
        PlanosMedicos = null;
        AgendamentosRealizados = null;

        Validate();
    }

    public void ChangeStatus(bool status)
    {
        Ativo = status;
        Paciente = null;
        PlanosMedicos = null;
        AgendamentosRealizados = null;
    }

    private void Validate()
    {
        DomainValidation.IdentifierGreaterThanZero(PacientePrincipalId, nameof(PacientePrincipalId));

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