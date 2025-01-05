using RSF.AgendamentoConsultas.Domain.Validation;
using RSF.AgendamentoConsultas.Shareable.Helpers;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class ConvenioMedico
{
    public int ConvenioMedicoId { get; private set; }
    public string Nome { get; private set; }
    public string Code { get; private set; }
    public string CodeOld { get; private set; }

    public ICollection<Cidade> CidadesAtendidas { get; set; }

    public ConvenioMedico(string nome)
    {
        Nome = nome;
        Code = Utilitarios.GenerateSlugifyString(nome);
        CodeOld = Utilitarios.GenerateSlugifyString(nome);

        Validate();
    }
    public ConvenioMedico(int id, string nome)
    {
        ConvenioMedicoId = id;
        Nome = nome;
        Code = Utilitarios.GenerateSlugifyString(nome);
        CodeOld = Utilitarios.GenerateSlugifyString(nome);

        Validate();
    }

    public void Update(string nome)
    {
        Nome = nome;
        Code = Utilitarios.GenerateSlugifyString(nome);
        CodeOld = Utilitarios.GenerateSlugifyString(nome);

        Validate();
    }

    private void Validate() => DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
}