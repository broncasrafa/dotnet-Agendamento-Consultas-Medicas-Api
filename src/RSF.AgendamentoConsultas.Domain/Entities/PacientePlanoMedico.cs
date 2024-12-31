using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class PacientePlanoMedico
{    
    public int PlanoMedicoId { get; private set; }
    public string NomePlano { get; private set; }
    public string NumCartao { get; private set; }
    public int? PacienteId { get; private set; }
    public int? PacienteDependenteId { get; private set; }
    public int ConvenioMedicoId { get; private set; }

    public Paciente Paciente { get; set; }
    public ConvenioMedico ConvenioMedico { get; set; }
    

    public PacientePlanoMedico(string nomePlano, string numCartao, int pacienteId, int pacienteDependenteId, int convenioMedicoId)
    {
        NomePlano = nomePlano;
        NumCartao = numCartao;
        PacienteId = pacienteId;
        PacienteDependenteId = pacienteDependenteId;
        ConvenioMedicoId = convenioMedicoId;

        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(NomePlano, nameof(NomePlano));
        DomainValidation.NotNullOrEmpty(NumCartao, nameof(NumCartao));
        DomainValidation.IdentifierGreaterThanZero(ConvenioMedicoId, nameof(ConvenioMedicoId));
        DomainValidation.IdentifierGreaterThanZero(PacienteId, nameof(PacienteId));
        DomainValidation.IdentifierGreaterThanZero(PacienteDependenteId, nameof(PacienteDependenteId));

    }
}