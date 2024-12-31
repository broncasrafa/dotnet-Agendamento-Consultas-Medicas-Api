using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class ConvenioMedicoCidade
{
    public int ConvenioMedicoCidadeId { get; private set; }
    public int ConvenioMedicoId { get; private set; }
    public int CidadeId { get; private set; }
    public int EstadoId { get; private set; }

    public ConvenioMedico ConvenioMedico { get; set; }
    public Cidade Cidade { get; set; }
    public Estado Estado { get; set; }

    public ConvenioMedicoCidade(int convenioMedicoId, int cidadeId, int estadoId)
    {
        ConvenioMedicoId = convenioMedicoId;
        CidadeId = cidadeId;
        EstadoId = estadoId;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.IdentifierGreaterThanZero(ConvenioMedicoId, nameof(ConvenioMedicoId));
        DomainValidation.IdentifierGreaterThanZero(CidadeId, nameof(CidadeId));
        DomainValidation.IdentifierGreaterThanZero(EstadoId, nameof(EstadoId));
    }
}