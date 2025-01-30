using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class EspecialistaEspecialidade
{
    public int Id { get; private set; }
    public int EspecialistaId { get; private set; }
    public int EspecialidadeId { get; private set; }
    public string TipoEspecialidade { get; private set; }

    public Especialista Especialista { get; set; }
    public Especialidade Especialidade { get; set; }

    protected EspecialistaEspecialidade()
    {
    }

    public EspecialistaEspecialidade(int especialistaId, int especialidadeId, string tipoEspecialidade)
    {
        EspecialistaId = especialistaId;
        EspecialidadeId = especialidadeId;
        TipoEspecialidade = tipoEspecialidade;

        Validate();
    }

    public EspecialistaEspecialidade(int especialidadeId, string tipoEspecialidade)
    {
        EspecialidadeId = especialidadeId;
        TipoEspecialidade = tipoEspecialidade;

        DomainValidation.IdentifierGreaterThanZero(EspecialidadeId, nameof(EspecialidadeId));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_ESPECIALIDADES, TipoEspecialidade, nameof(TipoEspecialidade));
    }

    public void Update(int especialidadeId, string tipoEspecialidade)
    {
        EspecialidadeId = especialidadeId;
        TipoEspecialidade = tipoEspecialidade;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.IdentifierGreaterThanZero(EspecialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(EspecialidadeId, nameof(EspecialidadeId));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_ESPECIALIDADES, TipoEspecialidade, nameof(TipoEspecialidade));
    }
}
