using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class EspecialistaEspecialidade
{
    public int Id { get; private set; }
    public int EspecialistaId { get; private set; }
    public int EspecialidadeId { get; private set; }
    public string TipoEspecialidade { get; private set; }

    public Especialista Especialista { get; set; }
    public Especialidade Especialidade { get; set; }

    public EspecialistaEspecialidade(int especialistaId, int especialidadeId, string tipoEspecialidade)
    {
        EspecialistaId = especialistaId;
        EspecialidadeId = especialidadeId;
        TipoEspecialidade = tipoEspecialidade;

        DomainValidation.IdentifierGreaterThanZero(especialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(especialidadeId, nameof(EspecialidadeId));
        DomainValidation.PossiblesValidTypes(TypeValids.VALID_ESPECIALIDADES, TipoEspecialidade, nameof(TipoEspecialidade));
    }
}
