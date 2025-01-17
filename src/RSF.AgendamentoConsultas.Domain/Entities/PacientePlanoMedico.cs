using Microsoft.Extensions.DependencyModel;

using RSF.AgendamentoConsultas.Domain.Validation;

namespace RSF.AgendamentoConsultas.Domain.Entities;

public class PacientePlanoMedico
{    
    public int PlanoMedicoId { get; private set; }
    public string NomePlano { get; private set; }
    public string NumCartao { get; private set; }
    public int PacienteId { get; private set; }
    public int ConvenioMedicoId { get; private set; }
    public bool? IsActive { get; private set; }

    public Paciente Paciente { get; set; }
    public ConvenioMedico ConvenioMedico { get; set; }


    protected PacientePlanoMedico()
    {        
    }

    public PacientePlanoMedico(string nomePlano, string numCartao, int pacienteId, int convenioMedicoId)
    {
        NomePlano = nomePlano;
        NumCartao = numCartao;
        PacienteId = pacienteId;
        ConvenioMedicoId = convenioMedicoId;
        IsActive = true;

        Validate();
    }

    public void Update(string nomePlano, string numCartao)
    {
        NomePlano = nomePlano;
        NumCartao = numCartao;

        Validate();
    }

    public void ChangeStatus(bool status) => IsActive = status;

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(NomePlano, nameof(NomePlano));
        DomainValidation.NotNullOrEmpty(NumCartao, nameof(NumCartao));
        DomainValidation.IdentifierGreaterThanZero(PacienteId, nameof(PacienteId));
        DomainValidation.IdentifierGreaterThanZero(ConvenioMedicoId, nameof(ConvenioMedicoId));
    }
}