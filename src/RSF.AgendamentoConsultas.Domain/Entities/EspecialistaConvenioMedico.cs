using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class EspecialistaConvenioMedico
{
    public int Id { get; private set; }
    public int EspecialistaId { get; private set; }
    public int ConvenioMedicoId { get; private set; }

    public Especialista Especialista { get; set; }
    public ConvenioMedico ConvenioMedico { get; set; }

    public EspecialistaConvenioMedico(int especialistaId, int convenioMedicoId)
    {
        EspecialistaId = especialistaId;
        ConvenioMedicoId = convenioMedicoId;

        DomainValidation.IdentifierGreaterThanZero(especialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(convenioMedicoId, nameof(ConvenioMedicoId));
    }
}