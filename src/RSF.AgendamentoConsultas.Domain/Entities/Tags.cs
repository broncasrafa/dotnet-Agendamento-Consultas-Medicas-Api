using RSF.AgendamentoConsultas.Domain.Utils;
using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class Tags
{
    public int TagId { get; private set; }
    public string Descricao { get; private set; }
    public string Code { get; private set; }

    public Tags(string descricao)
    {
        Descricao = descricao;
        Code = DomainUtil.GenerateSlugifyString(descricao);

        Validate();
    }

    public void Update(string descricao)
    {
        Descricao = descricao;
        Code = DomainUtil.GenerateSlugifyString(descricao);

        Validate();
    }

    private void Validate() 
        => DomainValidation.NotNullOrEmpty(Descricao, nameof(Descricao));
}