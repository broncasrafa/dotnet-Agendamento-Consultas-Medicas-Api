using RSF.AgendamentoConsultas.Domain.Utils;
using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

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

    public Especialidade(string nome, string nomePlural, int especialidadeGrupoId)
    {
        Nome = nome;
        NomePlural = nomePlural;
        Term = DomainUtil.GenerateSlugifyString(nome);
        Code = DomainUtil.GenerateSlugifyString(nome);
        CodeOld = DomainUtil.GenerateSlugifyString(nome);
        EspecialidadeGrupoId = especialidadeGrupoId;

        Validate();
    }

    public void Update(string nome, string nomePlural, int especialidadeGrupoId)
    {
        Nome = nome;
        NomePlural = nomePlural;
        Term = DomainUtil.GenerateSlugifyString(nome);
        Code = DomainUtil.GenerateSlugifyString(nome);
        CodeOld = DomainUtil.GenerateSlugifyString(nome);
        EspecialidadeGrupoId = especialidadeGrupoId;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Nome, nameof(Nome));
        DomainValidation.NotNullOrEmpty(NomePlural, nameof(NomePlural));
        DomainValidation.IdentifierGreaterThanZero(EspecialidadeGrupoId, nameof(EspecialidadeGrupoId));
    }
}