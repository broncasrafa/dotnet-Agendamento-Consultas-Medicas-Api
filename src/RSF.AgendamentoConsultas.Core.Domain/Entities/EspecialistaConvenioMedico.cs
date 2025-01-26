using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class EspecialistaConvenioMedico
{
    public int Id { get; private set; }
    public int EspecialistaId { get; private set; }
    public int ConvenioMedicoId { get; private set; }

    public Especialista Especialista { get; set; }
    public ConvenioMedico ConvenioMedico { get; set; }

    protected EspecialistaConvenioMedico()
    {
    }

    public EspecialistaConvenioMedico(int especialistaId, int convenioMedicoId)
    {
        EspecialistaId = especialistaId;
        ConvenioMedicoId = convenioMedicoId;

        Validate();
    }

    public void Update(int convenioMedicoId)
    {
        ConvenioMedicoId = convenioMedicoId;
        
        Validate();
    }

    private void Validate()
    {
        DomainValidation.IdentifierGreaterThanZero(EspecialistaId, nameof(EspecialistaId));
        DomainValidation.IdentifierGreaterThanZero(ConvenioMedicoId, nameof(ConvenioMedicoId));
    }
}