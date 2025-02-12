using RSF.AgendamentoConsultas.Core.Domain.Validation;
using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

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


    protected EspecialistaLocalAtendimento()
    {        
    }

    public EspecialistaLocalAtendimento(int id, int especialistaId, string nome, string logradouro, string complemento, string bairro, string cep, string cidade, string estado, decimal? preco, string tipoAtendimento, string telefone, string whatsapp)
    {
        DomainValidation.IdentifierGreaterThanZero(id, nameof(Id));

        Id = id;
        EspecialistaId = especialistaId;
        Nome = nome;
        Logradouro = logradouro;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
        Preco = preco;
        PrecoDescricao = Utilitarios.ConverterMoedaParaExtenso(Preco);
        TipoAtendimento = tipoAtendimento;
        Telefone = telefone;
        Whatsapp = whatsapp;

        Validate();
    }

    public EspecialistaLocalAtendimento(int especialistaId, string nome, string logradouro, string complemento, string bairro, string cep, string cidade, string estado, decimal? preco, string tipoAtendimento, string telefone, string whatsapp)
    {
        EspecialistaId = especialistaId;
        Nome = nome;
        Logradouro = logradouro;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
        Preco = preco;
        PrecoDescricao = Utilitarios.ConverterMoedaParaExtenso(Preco);
        TipoAtendimento = tipoAtendimento;
        Telefone = telefone;
        Whatsapp = whatsapp;

        Validate();
    }

    public void Update(string nome, string logradouro, string complemento, string bairro, string cep, string cidade, string estado, decimal? preco, string tipoAtendimento, string telefone, string whatsapp)
    {
        Nome = nome;
        Logradouro = logradouro;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
        Preco = preco;
        PrecoDescricao = Utilitarios.ConverterMoedaParaExtenso(Preco);
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
        DomainValidation.PossibleValidCep(Cep?.Replace(".","").Replace("-",""));
        DomainValidation.PossibleValidUF(Estado, nameof(Estado));
    }
}