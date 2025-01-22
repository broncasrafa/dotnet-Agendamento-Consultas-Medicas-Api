using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;
using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class EspecialidadeGrupo
{    
    public int EspecialidadeGrupoId { get; private set; }
    public string Nome { get; private set; }
    public string NomePlural { get; private set; }
    public string Code { get; private set; }

    public ICollection<Especialidade> Especialidades { get; set; }

    public EspecialidadeGrupo(string nome, string nomePlural)
    {
        Nome = nome;
        NomePlural = nomePlural;
        Code = Utilitarios.GenerateSlugifyString(nome);

        Validate();
    }

    public void Update(string nome, string nomePlural)
    {
        Nome = nome;
        NomePlural = nomePlural;
        Code = Utilitarios.GenerateSlugifyString(nome);

        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
        DomainValidation.NotNullOrEmpty(NomePlural, nameof(NomePlural));
    }
}