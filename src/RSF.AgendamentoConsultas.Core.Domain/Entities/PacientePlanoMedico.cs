﻿using RSF.AgendamentoConsultas.Core.Domain.Validation;

namespace RSF.AgendamentoConsultas.Core.Domain.Entities;

public class PacientePlanoMedico
{    
    public int PlanoMedicoId { get; private set; }
    public string NomePlano { get; private set; }
    public string NumCartao { get; private set; }
    public int PacienteId { get; private set; }
    public int ConvenioMedicoId { get; private set; }
    public bool? Ativo { get; set; }

    public Paciente Paciente { get; set; }
    public ConvenioMedico ConvenioMedico { get; set; }


    protected PacientePlanoMedico()
    {        
    }

    public PacientePlanoMedico(int id, string nomePlano, string numCartao, int pacienteId, int convenioMedicoId)
    {
        DomainValidation.IdentifierGreaterThanZero(id, nameof(PlanoMedicoId));

        PlanoMedicoId = id;
        NomePlano = nomePlano;
        NumCartao = numCartao;
        PacienteId = pacienteId;
        ConvenioMedicoId = convenioMedicoId;
        Ativo = true;

        Validate();
    }

    public PacientePlanoMedico(string nomePlano, string numCartao, int pacienteId, int convenioMedicoId)
    {
        NomePlano = nomePlano;
        NumCartao = numCartao;
        PacienteId = pacienteId;
        ConvenioMedicoId = convenioMedicoId;
        Ativo = true;

        Validate();
    }

    public void Update(string nomePlano, string numCartao)
    {
        NomePlano = nomePlano;
        NumCartao = numCartao;

        Validate();
    }

    public void ChangeStatus(bool status) => Ativo = status;

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(NomePlano, nameof(NomePlano));
        DomainValidation.NotNullOrEmpty(NumCartao, nameof(NumCartao));
        DomainValidation.IdentifierGreaterThanZero(PacienteId, nameof(PacienteId));
        DomainValidation.IdentifierGreaterThanZero(ConvenioMedicoId, nameof(ConvenioMedicoId));
    }
}