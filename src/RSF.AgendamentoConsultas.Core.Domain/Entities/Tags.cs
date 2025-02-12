using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;
using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class Tags
{
    public int TagId { get; private set; }
    public string Descricao { get; private set; }
    public string Code { get; private set; }

    public Tags(int id, string descricao)
    {
        DomainValidation.IdentifierGreaterThanZero(id, nameof(TagId));

        TagId = id;
        Descricao = descricao;
        Code = Utilitarios.GenerateSlugifyString(descricao);

        Validate();
    }

    public Tags(string descricao)
    {
        Descricao = descricao;
        Code = Utilitarios.GenerateSlugifyString(descricao);

        Validate();
    }

    public void Update(string descricao)
    {
        Descricao = descricao;
        Code = Utilitarios.GenerateSlugifyString(descricao);

        Validate();
    }

    private void Validate() 
        => DomainValidation.NotNullOrEmpty(Descricao, nameof(Descricao));
}