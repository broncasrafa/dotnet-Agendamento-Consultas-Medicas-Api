
using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class EspecialistaLocalAtendimento
{
    public int Id { get; private set; }
    public int EspecialistaId { get; private set; }
    public string Nome { get; private set; }
    public string Logradouro { get; private set; }
    public string Complemento { get; private set; }
    public string Bairro { get; private set; }
    public string Cep { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public decimal? Preco { get; private set; }
    public string PrecoDescricao { get; private set; }
    public string TipoAtendimento { get; private set; }
    public string Telefone { get; private set; }
    public string Whatsapp { get; private set; }

    public Especialista Especialista { get; set; }


    public EspecialistaLocalAtendimento(string nome, string logradouro, string complemento, string bairro, string cep, string cidade, string estado, decimal? preco, string precoDescricao, string tipoAtendimento, string telefone, string whatsapp)
    {
        Nome = nome;
        Logradouro = logradouro;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
        Preco = preco;
        PrecoDescricao = precoDescricao;
        TipoAtendimento = tipoAtendimento;
        Telefone = telefone;
        Whatsapp = whatsapp;

        Validate();
    }

    public void Update(string nome, string logradouro, string complemento, string bairro, string cep, string cidade, string estado, decimal? preco, string precoDescricao, string tipoAtendimento, string telefone, string whatsapp)
    {
        Nome = nome;
        Logradouro = logradouro;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
        Preco = preco;
        PrecoDescricao = precoDescricao;
        TipoAtendimento = tipoAtendimento;
        Telefone = telefone;
        Whatsapp = whatsapp;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.IdentifierGreaterThanZero(EspecialistaId, nameof(EspecialistaId));
        DomainValidation.PossibleValidPhoneNumber(Telefone, nameof(Telefone), isRequired: false);
        DomainValidation.PossibleValidPhoneNumber(Whatsapp, nameof(Whatsapp), isRequired: false);
        DomainValidation.PriceGreaterThanZero(Preco, nameof(Preco));
        DomainValidation.PossibleValidNumber(Cep?.Replace(".","").Replace("-",""), nameof(Cep));
    }
}