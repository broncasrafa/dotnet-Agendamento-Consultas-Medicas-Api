using RSF.AgendamentoConsultas.CrossCutting.Shareable.Helpers;
using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class Especialidade
{
    public int EspecialidadeId { get; private set; }
    public string Nome { get; private set; }
    public string NomePlural { get; private set; }
    public string Term { get; private set; }
    public string Code { get; private set; }
    public string CodeOld { get; private set; }
    public int EspecialidadeGrupoId { get; private set; }

    public EspecialidadeGrupo EspecialidadeGrupo { get; set; }
    public ICollection<Pergunta> Perguntas { get; set; }

    public Especialidade(string nome, string nomePlural, int especialidadeGrupoId)
    {
        Nome = nome;
        NomePlural = nomePlural;
        Term = Utilitarios.GenerateSlugifyString(nome);
        Code = Utilitarios.GenerateSlugifyString(nome);
        CodeOld = Utilitarios.GenerateSlugifyString(nome);
        EspecialidadeGrupoId = especialidadeGrupoId;

        Validate();
    }
    public Especialidade(int especialidadeId, string nome, string nomePlural, int especialidadeGrupoId)
    {
        EspecialidadeId = especialidadeId;
        Nome = nome;
        NomePlural = nomePlural;
        Term = Utilitarios.GenerateSlugifyString(nome);
        Code = Utilitarios.GenerateSlugifyString(nome);
        CodeOld = Utilitarios.GenerateSlugifyString(nome);
        EspecialidadeGrupoId = especialidadeGrupoId;

        Validate();
    }

    public void Update(string nome, string nomePlural, int especialidadeGrupoId)
    {
        Nome = nome;
        NomePlural = nomePlural;
        Term = Utilitarios.GenerateSlugifyString(nome);
        Code = Utilitarios.GenerateSlugifyString(nome);
        CodeOld = Utilitarios.GenerateSlugifyString(nome);
        EspecialidadeGrupoId = especialidadeGrupoId;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
        DomainValidation.IdentifierGreaterThanZero(EspecialidadeGrupoId, nameof(EspecialidadeGrupoId));
    }
}